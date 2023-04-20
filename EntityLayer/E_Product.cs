using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class E_Product
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public int Price { set; get; }
        public int Id { set; get; }
        public int Stocks { set; get; }
        public int SoldQuantity { set; get; }
    }
}
