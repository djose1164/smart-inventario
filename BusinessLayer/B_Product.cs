using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class B_Product
    {
        public bool addNewProduct(E_Product product)
        {
            return d_product.insert(product);
        }

        public bool modifyProduct(E_Product product)
        {
            return d_product.update(product);
        }

        public E_Product productByName(string name)
        {
            return d_product.getByName(name);
        }

        private D_Product d_product = new D_Product();
    }
}
