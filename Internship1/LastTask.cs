using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime LastLoginDate { get; set; }
    public List<string> ActionHistory { get; set; }

    public User(string username, string password, DateTime lastLoginDate)
    {
        Username = username;
        Password = password;
        LastLoginDate = lastLoginDate;
        ActionHistory = new List<string>();
    }
}

public class UserManager
{
    private const string FileName = "users.txt";
    private List<User> users;

    public List<User> Users { get => users; set => users = value; }

    public UserManager()
    {
        if (!File.Exists(FileName))
        {
            Users = new List<User>();
            var admin = new User("Admin", "AdminPassword", DateTime.Now);
            admin.ActionHistory.Add("Created Admin account.");
            Users.Add(admin);
            SaveUsers();
        }
        else
        {
            LoadUsers();
        }
    }

    private void LoadUsers()
    {
        Users = new List<User>();
        foreach (string line in File.ReadAllLines(FileName))
        {
            string[] parts = line.Split(',');
            string username = parts[0];
            string password = parts[1];
            DateTime lastLoginDate = DateTime.Parse(parts[2]);
            var user = new User(username, password, lastLoginDate);
            user.ActionHistory.AddRange(parts[3].Split(';'));
            Users.Add(user);
        }
    }

    private void SaveUsers()
    {
        using (StreamWriter writer = new StreamWriter(FileName))
        {
            foreach (User user in Users)
            {
                string actions = string.Join(";", user.ActionHistory);
                writer.WriteLine($"{user.Username},{user.Password},{user.LastLoginDate},{actions}");
            }
        }
    }

    public bool AuthenticateUser(string username, string password)
    {
        User user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            user.LastLoginDate = DateTime.Now;
            user.ActionHistory.Add($"{user.Username} - Login - {user.LastLoginDate.ToString("dd.MM.yyyy")}");
            SaveUsers();
            return true;
        }
        return false;
    }

    public bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        User user = Users.FirstOrDefault(u => u.Username == username && u.Password == oldPassword);
        if (user != null)
        {
            user.Password = newPassword;
            user.ActionHistory.Add($"{user.Username} - PasswordChange - {DateTime.Now.ToString("dd.MM.yyyy")}");
            SaveUsers();
            return true;
        }
        return false;
    }

    public void PrintUserHistory(string username)
    {
        User user = Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            Console.WriteLine("User Name - Action - Date");
            foreach (string action in user.ActionHistory)
            {
                Console.WriteLine(action);
            }
        }
        else
        {
            Console.WriteLine("The user is not found.");
        }
    }

    public void PrintAllUsers()
    {
        Console.WriteLine("List of all users:");
        foreach (User user in Users)
        {
            Console.WriteLine($"name: {user.Username}, password: {user.Password}, Date of last authorization: {user.LastLoginDate.ToString("dd.MM.yyyy")}");
        }
    }

    public void PrintUserInformation(string username)
    {
        User user = Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            Console.WriteLine($"Username: {user.Username}");
            Console.WriteLine($"Password: {user.Password}");
            Console.WriteLine($"Date of last authorization: {user.LastLoginDate.ToString("dd.MM.yyyy")}");
        }
        else
        {
            Console.WriteLine("User is not found.");
        }
    }

    public void DeleteUser(string username)
    {
        User user = Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            Users.Remove(user);
            SaveUsers();
            Console.WriteLine("The user has been successfully deleted.");
        }
        else
        {
            Console.WriteLine("User is not found.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        UserManager userManager = new UserManager();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Menu :");
            Console.WriteLine("1. Entry");
            Console.WriteLine("2. Change the password to an unauthorized user");
            Console.WriteLine("3.Change the password to an authorized user");
            Console.WriteLine("4. History output by user");
            Console.WriteLine("5. Get a list of all users");
            Console.WriteLine("6.Get user information ");
            Console.WriteLine("7. Delete user");
            Console.WriteLine("8. Exit");
            Console.Write("Enter the operation number: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the user name: ");
                    string username = Console.ReadLine();
                    Console.Write("enter your password: ");
                    string password = Console.ReadLine();
                    bool authenticated = userManager.AuthenticateUser(username, password);
                    if (authenticated)
                    {
                        Console.WriteLine("Successful authorization ");
                    }
                    else
                    {
                        Console.WriteLine("Incorrect username or password.");
                    }
                    break;

                case "2":
                    
                    break;

                case "3":
                    Console.Write("Enter the username: ");
                    string userToChangePassword = Console.ReadLine();
                    Console.Write("Enter the old password: ");
                    string oldPassword = Console.ReadLine();
                    Console.Write("Enter a new password: ");
                    string newPassword = Console.ReadLine();
                    bool passwordChanged = userManager.ChangePassword(userToChangePassword, oldPassword, newPassword);
                    if (passwordChanged)
                    {
                        Console.WriteLine("Password changed successfuly.");
                    }
                    else
                    {
                        Console.WriteLine("Incorrect username or old password.");
                    }
                    break;

                case "4":
                    Console.Write("Enter the username: ");
                    string userToShowHistory = Console.ReadLine();
                    userManager.PrintUserHistory(userToShowHistory);
                    break;

                case "5":
                    userManager.PrintAllUsers();
                    break;

                case "6":
                    Console.Write("Enter the user name: ");
                    string userToViewInfo = Console.ReadLine();
                    userManager.PrintUserInformation(userToViewInfo);
                    break;

                case "7":
                    Console.Write("Enter the username to delete: ");
                    string userToDelete = Console.ReadLine();
                    userManager.DeleteUser(userToDelete);
                    break;

                case "8":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Incorrect input. Try again.");
                    break;
            }
        }
    }
}





