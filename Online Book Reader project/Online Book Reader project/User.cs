using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader_project
{
   internal class User
    {
       internal string user_name;
       internal string email;
       internal string password;
        Reading_session read;
    internal  static Dictionary<User,Reading_session>history = new Dictionary<User,Reading_session>();
        internal void view_profile()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"User Name: {user_name}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Password: {password}");
            Console.WriteLine("----------------------------------------");
        }
        internal void Display_admin_view(User_Manger user, User admin_user)
        {
            while (true)
            {
                Console.WriteLine("\n===== Admin Menu =====");
                Console.WriteLine("1. Add Books");
                Console.WriteLine("2. View your profile");
                Console.WriteLine("3. Logout");
                Console.WriteLine("=====================");

                int op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        user.set_book(admin_user);
                        break;

                    case 2:
                        admin_user.view_profile();
                        break;

                    case 3:
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        internal void Display_customer_view(User_Manger user, User customer_user)
        {
            while (true)
            {
                Console.WriteLine("\n===== Customer Menu =====");
                Console.WriteLine("1. View current books in library");
                Console.WriteLine("2. View your profile");
                Console.WriteLine("3. View last reading sessions");
                Console.WriteLine("4. Read a book");
                Console.WriteLine("5. Get last page");
                Console.WriteLine("6. Logout");
                Console.WriteLine("=========================");

                if (!history.TryGetValue(customer_user, out read))
                {
                    read = new Reading_session();
                    history.Add(customer_user, read);
                }
                else
                {
                    read = history[customer_user];
                }

                int op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        if (User_Manger.books.Count > 0)
                        {
                            Console.WriteLine("\n===== Available Books =====");
                            for (int i = 0; i < User_Manger.books.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {User_Manger.books[i].title} by {User_Manger.books[i].Author}");
                            }
                            Console.WriteLine("===========================");
                        }
                        else
                        {
                            Console.WriteLine("\nNo books found in the library.");
                        }
                        break;

                    case 2:
                        customer_user.view_profile();
                        break;

                    case 3:
                        Console.WriteLine("\n===== Last Reading Session =====");
                        Console.WriteLine($"Last Book: {read.get_last_book()}");
                        Console.WriteLine($"Last Page: {read.get_current_bage()}");
                        Console.WriteLine($"Last Access Date: {read.get_last_date()}");
                        Console.WriteLine("===============================");
                        break;

                    case 4:
                        if (User_Manger.books.Count > 0)
                        {
                            Console.WriteLine("\n===== Available Books =====");
                            for (int i = 0; i < User_Manger.books.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {User_Manger.books[i].title} by {User_Manger.books[i].Author}");
                            }
                            Console.WriteLine("===========================");

                            Console.WriteLine("Choose a book by number:");
                            int num = int.Parse(Console.ReadLine());

                            if (num > 0 && num <= User_Manger.books.Count)
                            {
                                read.read_book(User_Manger.books[num - 1]);
                            }
                            else
                            {
                                Console.WriteLine("Invalid book number.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nNo books found in the library.");
                        }
                        break;

                    case 5:
                        Console.WriteLine($"\nLast Page Read: {read.get_current_bage()}");
                        break;

                    case 6:
                        if (history[customer_user] == null)
                        {
                            history.Add(customer_user, read);
                        }
                        else
                        {
                            history[customer_user] = read;
                        }
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

    }

}
