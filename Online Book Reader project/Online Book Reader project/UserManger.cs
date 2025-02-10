using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader_project
{
    public class User_Manger
    {
       private static Dictionary<string, Tuple<User, bool>> users = new Dictionary<string, Tuple<User, bool>>();
        User current_user;
        internal static List<Books> books=new List<Books>();
       internal void set_book(User current)
       {
            if (users[current.email].Item2==true)
            {
                Books Book = new Books();
                Console.WriteLine("Enter the author name");
                Book.Author = Console.ReadLine();

                Console.WriteLine("Enter the book name");
                Book.title = Console.ReadLine();

                Console.WriteLine("Enter number of pages");
                int n = int.Parse(Console.ReadLine());
                Book.pages = new string[n];
                for (int i = 0; i < n; i++)
                {
                    Book.pages[i] = $"Page{i+1}";
                }
                books.Add(Book);

            }
            else
            {
                Console.WriteLine("can't access");
            }
           
       }
       
        public void set_admins()
        {

            User user = new User();
            user.email="ahmedelgyar@gmail.com";
            user.password="gayar1351@23";
            user.user_name="Ahmed Elgayar";
            users.Add(user.email, Tuple.Create(user, true));
            user = new User();
            user.email="mohamedahmed@gmail.com";
            user.password="mohamed1351@23";
            user.user_name="Mohamed Ahmed";
            users.Add(user.email, Tuple.Create(user, true));

        }
        public void access()
        {
           
            Console.WriteLine("====================================");
            Console.WriteLine("   Welcome to **BookNest**!         ");
            Console.WriteLine("   Your Online Book Reader System   ");
            Console.WriteLine("====================================");

         
            Console.WriteLine("\nPlease choose an option:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Sign Up");
            Console.WriteLine("====================================");

      
            int choice = int.Parse(Console.ReadLine());

        
            switch (choice)
            {
                case 1:
                    login();
                    break;

                case 2:
                    sign_up();
                    break;

                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    access(); 
                    break;
            }
        }
        void sign_up()
        {
            Console.WriteLine("\n===== Sign Up =====");
            User user = new User();

            Console.WriteLine("Please enter your user name:");
            user.user_name = Console.ReadLine();

            Console.WriteLine("Please enter your email:");
            user.email = Console.ReadLine();

      
            while (users.ContainsKey(user.email))
            {
                Console.WriteLine("This email is already registered. Please enter another email:");
                user.email = Console.ReadLine();
            }

            Console.WriteLine("Please enter your password (at least 8 characters):");
            user.password = Console.ReadLine();

          
            while (user.password.Length < 8)
            {
                Console.WriteLine("Password must be at least 8 characters long. Please enter a new password:");
                user.password = Console.ReadLine();
            }

            users.Add(user.email, Tuple.Create(user, false));
            current_user = user;

            Console.WriteLine("\nSign up successful! Welcome, " + user.user_name + "!");
        }
        void login()
        {
            Console.WriteLine("\n===== Login =====");
            Console.WriteLine("Please enter your email:");
            string em = Console.ReadLine();

            if (!users.ContainsKey(em))
            {
                Console.WriteLine("This email is not registered. Please try again or sign up.");
                access(); 
                return;
            }

            Console.WriteLine("Please enter your password:");
            string ps = Console.ReadLine();

            while (users[em].Item1.password != ps)
            {
                Console.WriteLine("Invalid password. Please try again:");
                ps = Console.ReadLine();
            }

            Console.WriteLine("\nLogin successful! Welcome back, " + users[em].Item1.user_name + "!");
            current_user = users[em].Item1;
        }
        internal User get_current_user()
        {

            return current_user;
        }
        internal bool is_admin(User user)
        {
            return users[user.email].Item2==true;
        }
        internal static void LoadBooksFromFiles()
        {
            string booksFilePath = Path.Combine(Directory.GetCurrentDirectory(), "books_data.txt");
            if (File.Exists(booksFilePath))
            {
                string[] lines = File.ReadAllLines(booksFilePath);
                Books currentBook = null;

                foreach (string line in lines)
                {
                    if (line.StartsWith("Title:"))
                    {
                        if (currentBook != null)
                        {
                            books.Add(currentBook);
                        }
                        currentBook = new Books();
                        currentBook.title = line.Substring("Title:".Length).Trim();
                    }
                    else if (line.StartsWith("Author:"))
                    {
                        currentBook.Author = line.Substring("Author:".Length).Trim();
                    }
                    else if (line.StartsWith("Pages:"))
                    {
                        int pageCount = int.Parse(line.Substring("Pages:".Length).Trim());
                        currentBook.pages = new string[pageCount];
                    }
                    else if (line.StartsWith("Page"))
                    {
                        int pageNumber = int.Parse(line.Split(':')[0].Substring("Page".Length).Trim());
                        string pageContent = line.Split(':')[1].Trim();
                        currentBook.pages[pageNumber - 1] = pageContent;
                    }
                }


                if (currentBook != null)
                {
                    books.Add(currentBook);
                }
            }
        }


    }
}
