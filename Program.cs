namespace Login_system
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List<User> users = new List<User> {
            new User("awde","secretawde"),
            new User("admin","admin321"),
            new User("lego","foxy")
            };

            Console.WriteLine("Do you want to create a new user? (y/n)");
            string addUserInput = Console.ReadLine()?.ToLower();

            if (addUserInput == "y")
            {
                Console.Clear();
                string newUser;
                while (true)
                {
                    Console.WriteLine("Enter new username:");
                    newUser = Console.ReadLine();

                    if (users.Exists(user => user.Username == newUser))
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

                Console.WriteLine("Enter new password:");
                string newPass = ReadPassword();

                users.Add(new User(newUser, newPass));

                Console.Clear();
                Console.WriteLine($"User '{newUser}' added successfully!\n\n");
            }


            Console.WriteLine("Please enter your username:");
            string userName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Please enter your password:");
            string password = ReadPassword();
            Console.Clear();

            bool isAuthenticated = users.Exists(user => user.Username == userName && user.Password == password);

            if (isAuthenticated)
            {
                Console.WriteLine("You have correctly logged in!");
            }
            else
            {
                Console.WriteLine("Username or password is wrong");
            }

            Console.ReadKey();
        }
       
        static string ReadPassword()
        {
            string password = "";
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return password;

                }
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password[..^1];
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("*");

                }
            }
        }
    }
}
