/*
Assessment 2
Group 6
Members: Jacob David, Hana Wong, Hieu Vu
Code Document Owner: Jacob David
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Collections.Generic;

namespace BankOfSydney
{
    //Created a class Bank to hold main, login, check password, and display login successful
    public class BankApp
    {
        static void Main()
        {
            //create an instance of the SignUp class
            BankApp signUp = new BankApp();
            BankApp bank = new BankApp();
            //create a test user
            BankApp.Users.Add(new User(username: "Joe.Doe", password: "Password123", email: "Joe.Doe@fakeemail.com", phone: "0412345678", age: "32", name: "Joe Doe"));
            // main method to run the program
            //loop to keep running the program until user quits
            while (true)
            {
                bool running = true;
                Console.WriteLine("Welcome! You are with the Bank of Sydney!");
                while (running)
                {
                    Console.WriteLine("Please select an option:");
                    Console.WriteLine("1. Login");
                    Console.WriteLine("2. Sign Up");
                    Console.WriteLine("3. Exit");
                    Console.Write("Enter your choice: ");
                    string option = Console.ReadLine() ?? string.Empty;
                    switch (option)
                    {
                        case "1":
                            Console.WriteLine("Login selected");
                            Console.Write("Enter username: ");
                            string username = Console.ReadLine() ?? string.Empty;
                            Console.Write("Enter password: ");
                            string password = ReadPassword();
                            //if login is successful, call the LoginSuccessful method
                            if (bank.Login(username, password))
                            {
                                bank.LoginSuccessful(username);
                            }
                            else
                            {
                                Console.WriteLine("\nPress Enter to continue...");
                                Console.ReadLine();
                            }
                            Console.Clear();
                            break;
                        //calls the login method

                        case "2":
                            Console.Clear();
                            Console.WriteLine("Sign Up selected");
                            signUp.SignUpMethod();
                            Console.WriteLine("\nPress Enter to continue...");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        //calls the signup method

                        case "3":
                            Console.Clear();
                            Console.WriteLine("Exit selected");
                            running = false;
                            return;
                        //exits the program

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                            //if user enters option other than 1, 2, or 3
                            // prompt user to try again
                            //default 
                    }
                }
            }
        }
        public bool Login(string username, string password)
        {
            var user = BankApp.Users.Find(u => u.Username == username);

            if (user == null)
            {
                Console.Clear();
                Console.WriteLine("Error: User not found");
                return false;
            }

            int attempts = 0;
            while (attempts < 3)
            {
                if (user.Password == password)
                //verify the password is correct
                {
                    Console.WriteLine("Login successful!");
                    return true;
                }
                else
                {
                    attempts++;
                    if (attempts < 3)
                    {
                        Console.WriteLine($"Error: Incorrect password. You have {3 - attempts} attempt(s) remaining.");
                        Console.Write("Re-enter password: ");
                        password = ReadPassword();
                        //user has a limit of 3 attempts to enter the correct password for Joe.Doe's bank account (security)
                    }
                }
            }

            Console.WriteLine("Error: Maximum login attempts exceeded. Returning to the main menu.");
            return false;
            //loop restarts
        }
        private static string ReadPassword()
        //privately display the password as asterisks
        //this is for our highly-regulated security measures at The Bank of Sydney
        {
            string password = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[0..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
        //create a method to display the login successful page
        public void LoginSuccessful(string username)
        {
            Console.Clear();
            Console.WriteLine("Login successful!");
            //create a loop to keep displaying the account management options until user logs out
            bool choosingOption = true;
            while (choosingOption)
            {
                Console.WriteLine("Account Management:");
                //not necessary but nice touch to display the user's name
                var user = BankApp.Users.Find(u => u.Username == username);
                Console.WriteLine($"Welcome, {user?.Name ?? username}!");
                //display the account management options
                Console.WriteLine("1. View Account Info");
                Console.WriteLine("2. View Balance");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. Logout");
                string choice = Console.ReadLine();
                //store the user's choice in a variable
                //use a switch statement to determine which option the user selected
                switch (choice)
                {
                    //currently the only option that works, displays the user's account information
                    case "1":
                        User userInfo = BankApp.Users.Find(u => u.Username == username);
                        userInfo.UserInfo();
                        Console.WriteLine("Press Enter to return to Account Management...");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    //placeholders for the other options
                    case "2":
                        Console.WriteLine("Feature coming soon!");
                        break;
                    case "3":
                        Console.WriteLine("Feature coming soon!");
                        break;
                    case "4":
                        Console.WriteLine("Feature coming soon!");
                        break;
                    case "5":
                        Console.WriteLine("Feature coming soon!");
                        break;
                    //logout option that returns the user to the main menu
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Logout successful!");
                        choosingOption = false;
                        break;
                    //invalid option that prompts the user to try again
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option.");
                        continue;
                }
            }
                Console.WriteLine("Press Enter to return to main menu...");
                Console.ReadLine();   
        }

    }
    //Created a class SignUp to call back to from main
    //SignUp holds the sign up method
    class BankApp
    {
        //create private variables to store user information in the class
        private string username, password, email, phone, age, name;
        //create a list to store user information
        public static List<User> Users = new List<User>();
        //create a method to sign up, validate user information and store it in the user list
        public void SignUpMethod()
        {
            do
            {
                Console.WriteLine("Enter your name: ");
                //Storing user input in name variable
                name = Console.ReadLine();
                //Checking if the name is empty
                if (string.IsNullOrEmpty(name))
                {
                    //Clean the console
                    Console.Clear();
                    Console.WriteLine("Sign Up");
                    //Displaying error message
                    Console.WriteLine("Name cannot be empty");
                }
            } while (string.IsNullOrEmpty(name));
            do
            {
                //Prompting user to enter username
                Console.WriteLine("Enter your username: ");
                //Storing user input in username variable
                username = Console.ReadLine();
                //Checking if the username is empty
                if (string.IsNullOrEmpty(username))
                {
                    //clean the console
                    Console.Clear();
                    Console.WriteLine("Sign Up");
                    //Displaying error message
                    Console.WriteLine("Username cannot be empty");
                }
            } while (string.IsNullOrEmpty(username));
            do
            {
                //Prompting user to enter email
                Console.WriteLine("Enter your email: ");
                //Storing user input in email variable
                email = Console.ReadLine();
                //Checking if the email is empty
                if (string.IsNullOrEmpty(email) || !email.Contains("@") || !email.EndsWith(".com"))
                {
                    //Clean the console
                    Console.Clear();
                    Console.WriteLine("Sign Up");
                    //Displaying error message
                    Console.WriteLine("Must be a Valid Email");
                }
            } while (string.IsNullOrEmpty(email) || !email.Contains("@") || !email.EndsWith(".com"));
            do
            {
                //Prompting user to enter password
                Console.WriteLine("Enter your password: ");
                //Storing user input in password variable
                password = Console.ReadLine();
                //Checking if the password is empty
                if (string.IsNullOrEmpty(password))
                {
                    //Clean the console
                    Console.Clear();
                    Console.WriteLine("Sign Up");
                    //Displaying error message
                    Console.WriteLine("Password cannot be empty");
                }
            } while (string.IsNullOrEmpty(password));
            do
            {
                //Prompting user to enter phone
                Console.WriteLine("Enter your phone: ");
                //Storing user input in phone variable
                phone = Console.ReadLine();
                //Checking if the phone is empty or contains only numbers
                if (string.IsNullOrEmpty(phone) || !phone.All(char.IsDigit))
                {
                    //Clean the console
                    Console.Clear();
                    Console.WriteLine("Sign Up");
                    //Displaying error message
                    Console.WriteLine("Phone number must not be empty and must only contain numbers");
                }
            } while (string.IsNullOrEmpty(phone) || !phone.All(char.IsDigit));
            do
            {
                //Prompting user to enter age
                Console.WriteLine("Enter your age: ");
                //Storing user input in age variable
                age = Console.ReadLine();
                //Checking if the age is empty or contains only numbers
                if (string.IsNullOrEmpty(age) || !age.All(char.IsDigit))
                {
                    //Clean the console
                    Console.Clear();
                    Console.WriteLine("Sign Up");
                    //Displaying error message
                    Console.WriteLine("Age must not be empty and must only contain numbers");
                }
            } while (string.IsNullOrEmpty(age) || !age.All(char.IsDigit));
            //create a new user profile using the information collected
            User newUser = new User(username, password, email, phone, age, name);
            //add the new user to the list of users
            Users.Add(newUser);
            //clean the console and display user information
            Console.Clear();
            Console.WriteLine("Account Successfully Created!");
            Console.WriteLine("Name : " + name);
            Console.WriteLine("Username: " + username);
            Console.WriteLine("Password: " + password);
            Console.WriteLine("Email: " + email);
            Console.WriteLine("Phone: " + phone);
            Console.WriteLine("Age: " + age);

        }
    }
    //Created a class User to hold user information
    public class User
    {
        //create public variables to store user information
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        //create a constructor to initialize user information
        public User(string username, string password, string email, string phone, string age, string name)
        {
            Username = username;
            Password = password;
            Email = email;
            Phone = phone;
            Age = age;
            Name = name;
        }
        //create a method to display user information
        public void UserInfo()
        {
            Console.WriteLine("Username: " + Username);
            Console.WriteLine("Password: " + Password);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Phone: " + Phone);
            Console.WriteLine("Age: " + Age);
        }

    }
}