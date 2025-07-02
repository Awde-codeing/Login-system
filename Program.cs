using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Login_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "users.csv";

            // Load users from CSV file created above
            List<User> users = LoadUsersFromCsv(filePath);

            // If file was empty , add default users and save
            if (users.Count == 0)
            {
                //usues the list users for default users
                users = new List<User> {
                    new User("awde", "secretawde"),
                    new User("admin", "admin321"),
                    new User("lego", "foxy")
                };
                //saves default users to the file
                SaveUsersToCsv(users, filePath);
            }
            //prompts to create user using Y for yes and N for no
            //takes the user input even if u write in caps and then converts it to lower to make sure that it does not break
            Console.WriteLine("Do you want to create a new user? (y/n)");
            string addUserInput = Console.ReadLine()?.ToLower();
            //if statement that checks what you type if its y or n
            if (addUserInput == "y")
            {
                //creates a string called new user
                Console.Clear();
                string newUser;
                while (true)
                {
                    //while true to make sure ur ina loop until you enter a username without a comma and that it uses a real username
                    Console.WriteLine("Enter new username (no commas allowed):");
                    newUser = Console.ReadLine();
                    //if statement to check if the username contains a comma
                    if (newUser.Contains(","))
                    {
                        Console.WriteLine("Username can't contain commas. Please try again.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    //checks the if the user list exists, user=> username..... means that for each user in the list it checks if user.username is equals the value of newUser
                    if (users.Exists(user => user.Username == newUser))     //uses a lamba expression basically meaning true or false in very simplified terms
                        //if any user has a username matching the new user itreturns the true otherwise its a false
                    {
                        Console.WriteLine($"Username '{newUser}' is already taken. Please try again.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else
                    {
                        break;
                    }
                }
                //creates a string called new pass for the password of a new user
                string newPass;
                while (true)
                {
                    //while true loop for the same but just with a password instead of username
                    Console.WriteLine("Enter new password (no commas allowed):");
                    newPass = ReadPassword();
                    //literally the same as username but if its a password instead
                    if (newPass.Contains(","))
                    {
                        Console.WriteLine("Password cannot contain commas. Please try again.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    break;
                }
                //adds the newly made user
                users.Add(new User(newUser, newPass));

                // Save updated users list to CSV file
                SaveUsersToCsv(users, filePath);
                //clears console from the previous texts and allows you to go to login area
                Console.Clear();
                Console.WriteLine($"User '{newUser}' added successfully!\n\n");
            }
            //login area where you type in user and password correctly
            Console.WriteLine("Please enter your username:");
            string userName = Console.ReadLine();
            Console.Clear();
            //reads password and makes it a string for the bool below
            Console.WriteLine("Please enter your password:");
            string password = ReadPassword();
            Console.Clear();
            //bool to check that users exist and the username and password fits correctly
            bool isAuthenticated = users.Exists(user => user.Username == userName && user.Password == password);
            //checks if everything is writtent correctly with an if statement
            if (isAuthenticated)
            {
                Console.WriteLine("You have correctly logged in!");
            }
            //if you type wrong it will not allow you to log in
            else
            {
                Console.WriteLine("Username or password is wrong");
            }

            Console.ReadKey();
        }
        //area for saving to csv file, using var as somewhat confused to what i should change it out with variable wise
        static void SaveUsersToCsv(List<User> users, string filePath)
        {

            var lines = new List<string>();
            foreach (var user in users)
            {
                // Just write's username and password separated by comma, in csv file
                lines.Add($"{user.Username},{user.Password}");
            }
            File.WriteAllLines(filePath, lines);
        }
        //loads the users from csv file so that you can actually use the newly created user
        static List<User> LoadUsersFromCsv(string filePath)
        {
           //create an empty list to store the users
            var users = new List<User>();
            //checks if the file exists at the file path calling on the filepath.userscsv
            if (!File.Exists(filePath))
                //returns an empty list if the file does not exist
                return users;

            var lines = File.ReadAllLines(filePath);    //read all lines from the csv file, (into an array of strings)
            foreach (var line in lines)                 //go over every line in the file
            {
                var parts = line.Split(',');            //splits lines by commas into an array of parts user and password
                if (parts.Length == 2)                  //checks if the line contains exactly the two parts
                {
                    users.Add(new User(parts[0], parts[1])); //creates a new user with the two parts and adds it to the list
                }
            }
            return users;                               //returns the list with the user object, (just the users)
        }

        static string ReadPassword()                    //Reads the password making sure its the real password
        {
            string password = "";                       //creates an empty string for password
            while (true)                                //keeps the while loop running until you have been logged in
            {
                var key = Console.ReadKey(true);        //checks if the keyboard gets used to type in something
                if (key.Key == ConsoleKey.Enter)        //checks if the key press is an enter
                {
                    Console.WriteLine();                //outputs userinput
                    return password;                    //returns the userinput 
                }
                if (key.Key == ConsoleKey.Backspace)    //checks if they keypress is a backspace
                {
                    if (password.Length > 0)            //says if the length of the password is above 0 characters long
                    {
                        password = password[..^1];      //it means take everything except the last character of the password in short removing the last letter of the string
                        Console.Write("\b \b");         //\b \b is quite the smart thing let me tell you why
                                                        //\b moves the curser back
                                                        /*does not remove anything
                                                        so therefor we print the space between those 2 as to remove the *
                                                        and \b goes back one more*/
                    }
                }
                else
                {
                    password += key.KeyChar;            //if the password is equal or adding a key press this checks for it
                    Console.Write("*");                 //this makes it so whenever  the password gets inputted it gets hidden
                }
            }
        }
    }
}
