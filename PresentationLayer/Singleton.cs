using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using BusinessLayer;

namespace PresentationLayer
{
    public sealed class Singleton
    {
        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton();
                return instance;
            }
        }

        public E_User User { set; get; }

        public void fillUser(E_User user)
        {
            User = b_user.getByEmail(user);
        }

        private static Singleton instance = null;
        private B_User b_user = new B_User();
    }
}
