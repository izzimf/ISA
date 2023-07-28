using System.Text.RegularExpressions;
using System.Xml;

class Program
{
    static List<User> users = new List<User>();
    static string usersFileName = "users.json"; // File to store user data in JSON format

    public static object JsonConvert { get; private set; }

    static void Main(string[] args)
    {
        LoadUsersFromFile(); // Load user data from the file
        Console.WriteLine("Welcome!");

        while (true)
        {
            Console.WriteLine("Do you want to log in (L), sign in (R), or exit (E)?");
            string input = Console.ReadLine();

            switch (input.ToUpper())
            {
                case "L":
                    Login();
                    break;
                case "R":
                    Register();
                    break;
                case "E":
                    SaveUsersToFile(); // Save user data to the file before exiting
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }
    }

    static void LoadUsersFromFile()
    {
        if (File.Exists(usersFileName))
        {
            string json = File.ReadAllText(usersFileName);
            users = JsonConvert.DeserializeObject<List<User>>(json);
        }
    }

    static void SaveUsersToFile()
    {
        string json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(usersFileName, json);
    }

    static void Login()
    {
        Console.WriteLine("Enter login:");
        string username = Console.ReadLine();

        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();

        User user = users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            if (user.Password == password)
            {
                user.LastLogin = DateTime.Now;
                user.LoginCount++;
                Console.WriteLine("Authorization successful.");
                ShowUserInfo(user);
            }
            else
            {
                Console.WriteLine("Incorrect password.");
            }
        }
        else
        {
            Console.WriteLine("User is not found.");
        }
    }

    static void Register()
    {
        Console.WriteLine("Enter login (only English letters, digits, or _):");
        string username = Console.ReadLine();

        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$") || username.Length < 6)
        {
            Console.WriteLine("Invalid login format or length (at least 6 characters, only English letters, digits, or _).");
            return;
        }

        Console.WriteLine("Enter your password (at least 6 characters, with upper and lower case letters, and a digit or _):");
        string password = Console.ReadLine();

        if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d|_).{6,}$"))
        {
            Console.WriteLine("Invalid password format.");
            return;
        }

        Console.WriteLine("Enter password confirmation:");
        string confirmPassword = Console.ReadLine();

        if (password != confirmPassword)
        {
            Console.WriteLine("Password mismatch");
            return;
        }

        User newUser = new User(username, password);
        users.Add(newUser);

        Console.WriteLine("Registration successful.");
        ShowUserInfo(newUser);
    }

    static void ShowUserInfo(User user)
    {
        Console.WriteLine("Information about User:");
        Console.WriteLine("Number of authorizations: " + user.LoginCount);
        Console.WriteLine("Date of registration: " + user.RegistrationDate);
        Console.WriteLine("Last login date: " + user.LastLogin);

        if (user.Username == "Admin" && user.Password == "Admin123")
        {
            Console.WriteLine("\nAll Users Information:");
            foreach (var u in users)
            {
                Console.WriteLine($"{u.Username}: Authorizations - {u.LoginCount}, Registration Date - {u.RegistrationDate}, Last Login - {u.LastLogin}");
            }
        }
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
        LoginCount = 0;
    }
}