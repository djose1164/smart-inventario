using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class B_User
    {
        public bool verifyUser(E_User user)
        {
            return d_user.verifyUser(user);
        }

        public bool createUser(E_User user)
        {
            return d_user.createUser(user);
        }

        public E_User getByEmail(E_User user)
        {
            return d_user.getByEmail(user.Email);
        }

        public bool updateUser(E_User user)
        {
            return d_user.update(user);
        }

        private D_User d_user = new D_User();
    }
}
