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

        public bool modifyProduct(E_Product product, bool partial=false)
        {
            return d_product.update(product, partial);
        }

        public E_Product productByName(string name)
        {
            return d_product.getByName(name);
        }

        public bool processTransaction(E_Product product)
        {
            var fetched = d_product.getByName(product.Name);
            if (fetched.Stocks < 1)
                return false;

            --fetched.Stocks;
            ++fetched.SoldQuantity;
            return d_product.update(fetched, false);
        }

        public bool deleteProduct(E_Product product)
        {
            return d_product.delete(product);
        }

        private D_Product d_product = new D_Product();
    }
}
