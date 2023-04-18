using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class E_User
    {
        public string Email { set; get; }
        public string Password { set; get; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public bool IsAdmin { set; get; }
    }
}
