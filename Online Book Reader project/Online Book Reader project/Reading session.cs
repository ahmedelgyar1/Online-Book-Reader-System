using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader_project
{
    internal class Reading_session
    {
        int current_page;
        DateTime last_access_date;
        string last_book;
      internal  Books book;
        internal void read_book(Books book2)
        {
            this.last_book = book2.title;
            this.last_access_date = DateTime.Now;

            for (int i = 0; i < book2.pages.Length; i++)
            {
                Console.WriteLine("\n===== Page Content =====");
                Console.WriteLine(book2.pages[i]);
                Console.WriteLine("=======================");

                if (i < book2.pages.Length - 1)
                {
                    Console.WriteLine("Do you want to continue? (1 - Yes, 2 - No)");
                    string ans = Console.ReadLine();
                    if (ans == "2")
                    {
                        this.current_page = i + 1;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("THE END");
                }
            }
        }
        internal int  get_current_bage()
        {
            return this.current_page;
        }
        internal string get_last_book()
        {
            return this.last_book;
        }
        internal DateTime get_last_date()
        {
            return last_access_date;
        }

        
    }
}
