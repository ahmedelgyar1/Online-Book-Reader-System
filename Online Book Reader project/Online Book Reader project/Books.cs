using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader_project
{
    internal class Books
    {
       internal string title;
       internal string Author;
       internal string[]pages;
        internal void details_about_book()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Number of Pages: {pages.Length}");
            Console.WriteLine("----------------------------------------");
        }
    }
}
