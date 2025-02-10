using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Book_Reader_project
{
    public class Book_system
    {
        User_Manger system;
      public Book_system() { 
        
         system = new User_Manger();
            system.set_admins();
            User_Manger.LoadBooksFromFiles();

      }
        public void Run_system()
        {
            while (true) {
                system.access();
               User user= system.get_current_user();
                if(system.is_admin(user))
                {
                  user.Display_admin_view(system,user);
                }
                else
                {
                    user.Display_customer_view(system,user);
                }
                    

            }
        }

    }
}
