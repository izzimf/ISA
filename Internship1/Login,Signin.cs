using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<string, User> users = new Dictionary<string, User>();

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome!");
        Console.WriteLine("Do you want to log in (L) or sign in (R)?");

        string input = Console.ReadLine();

        switch (input.ToUpper())
        {
            case "L":
                Login();
                break;
            case "R":
                Register();
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }

    static void Login()
    {
        Console.WriteLine("Enter login:");
        string username = Console.ReadLine();

        Console.WriteLine("Enter your passsword:");
        string password = Console.ReadLine();

        if (users.ContainsKey(username))
        {
            User user = users[username];
            if (user.Password == password)
            {
                user.LastLogin = DateTime.Now;
                Console.WriteLine("Authorization successful.");
                ShowUserInfo(user);
            }
            else
            {
                Console.WriteLine("Incorrect passsword.");
            }
        }
        else
        {
            Console.WriteLine("User is not found.");
        }
    }

    private static void ShowUserInfo(User user)
    {
        throw new NotImplementedException();
    }

    static void Register()
    {
        Console.WriteLine("Enter login:");
        string username = Console.ReadLine();

        if (users.ContainsKey(username))
        {
            Console.WriteLine("User with this login is already exist.");
            return;
        }

        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();

        Console.WriteLine("Enter password confirmation:");
        string confirmPassword = Console.ReadLine();

        if (password != confirmPassword)
        {
            Console.WriteLine("Password mismatch");
            return;
        }

        User newUser = new User(username, password);
        users.Add(username, newUser);

        Console.WriteLine("Registration successful.");
        ShowUserInfo(newUser);
        Main(null); 

    static void ShowUserInfo(User user)
    {
        Console.WriteLine("Information about User:");
        Console.WriteLine("Number of authorizations: " + user.LoginCount);
        Console.WriteLine("Date of registration: " + user.RegistrationDate);
        Console.WriteLine("Last login date: " + user.LastLogin);
    }
}

    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int LoginCount { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLogin { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            RegistrationDate = DateTime.Now;
            LastLogin = DateTime.Now;
        }
    }
}
