﻿using System;
using System.Collections.Generic;

namespace Practicant
{

    //Задание: 
    //Для регистрации реализовать условие для пароля и логина: допустимые символы(только англ. буквы, цифры или знак _). Количество символов не менее 6. 
    //Хотя бы один символ должен быть в нижнем и в верхнем регистре. Обязательно должна присутствовать цифра или _. 
    //Данные после регистрации должны сохранятся в текстовый файл. При авторизации пользователя данные по последней авторизации и количеству авторизаций должны обновляться
    //При обновлении данных файл должен обновляться, но данные по остальным пользователям не должны удаляться.
    //Необходимо реализовать пользователя: Login - Admin, Password - Admin123. После авторизации которого должна отображаться информация по всем пользователям.
    //Для обычного пользователя информация доступна только по его логину.

    class Program
    {
        static Dictionary<string, User> users = new Dictionary<string, User>();

        static void Main(string[] args)
        {
            //Необходимо сделать бесконечный цикл с условием выхода
            Console.WriteLine("Welcome!");
            Console.WriteLine("Do you want to log in (L) or sign in (R)?");

            //сделать проверку ввода на допустимые символы (только англ. буквы, цифры или знак _ )
            //проверку вынести в отдельный метод
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
            //сделать проверку ввода на допустимые символы (только англ. буквы, цифры или знак _ )
            Console.WriteLine("Enter login:");
            string username = Console.ReadLine();

            //сделать проверку ввода на допустимые символы (только англ. буквы, цифры или знак _ )
            Console.WriteLine("Enter your passsword:");
            string password = Console.ReadLine();

            //проверку вынести в отдельный метод
            if (users.ContainsKey(username))
            {
                User user = users[username];
                //проверку вынести в отдельный метод
                if (user.Password == password)
                {
                    user.LastLogin = DateTime.Now;
                    Console.WriteLine("Authorization successful.");
                    //Метод реализовать в классе User
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

        // Реализован дважды
        private static void ShowUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        static void Register()
        {
            //сделать проверку ввода на допустимые символы (только англ. буквы, цифры или знак _ )
            Console.WriteLine("Enter login:");
            string username = Console.ReadLine();

            //проверку вынести в отдельный метод
            if (users.ContainsKey(username))
            {
                Console.WriteLine("User with this login is already exist.");
                return;
            }

            //сделать проверку ввода на допустимые символы (только англ. буквы, цифры или знак _ )
            //проверку вынести в отдельный метод
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            //сделать проверку ввода на допустимые символы (только англ. буквы, цифры или знак _ )
            //проверку вынести в отдельный метод
            Console.WriteLine("Enter password confirmation:");
            string confirmPassword = Console.ReadLine();

            //проверку вынести в отдельный метод
            if (password != confirmPassword)
            {
                Console.WriteLine("Password mismatch");
                return;
            }

            //лучше создать отдельный класс UserAuthentication где будет метод добавления пользователя и др. методы по работе с пользователем
            User newUser = new User(username, password);
            users.Add(username, newUser);

            Console.WriteLine("Registration successful.");

            
            ShowUserInfo(newUser);

            //Метод Main является точкой входа и не используется внутри приложения, вывод начального экрана необходимо реализовать 
            // за счет бесконечного цикла
            Main(null);

            //Метод лучше добавить классу User, доступ к его свойствам будет сразу через this
            static void ShowUserInfo(User user)
            {
                Console.WriteLine("Information about User:");
                Console.WriteLine("Number of authorizations: " + user.LoginCount);
                Console.WriteLine("Date of registration: " + user.RegistrationDate);
                Console.WriteLine("Last login date: " + user.LastLogin);
            }
        }

        //класс необходимо вынести в отдельный файл
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
}