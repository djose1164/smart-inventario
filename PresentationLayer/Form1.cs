using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using EntityLayer;

namespace PresentationLayer
{
    public partial class Form1 : Form
    {
        private B_Product b_product = new B_Product();
        private B_User b_user = new B_User();
        private E_Product foundProuct;
        private Singleton instance = Singleton.Instance;

        public Form1()
        {
            InitializeComponent();
        }

        private void passwordField_OnValueChanged(object sender, EventArgs e)
        {
            passwordField.isPassword = true;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            E_User user = new E_User();
            user.Email = usernameField.Text;
            user.Password = passwordField.Text;

            if (b_user.verifyUser(user))
            {
                instance.fillUser(user);
                if (!instance.User.IsAdmin)
                    usersBtnMenu.Visible = false;

                tabControl.SelectedTab = homePage;
            }
            else
                MessageBox.Show("Usuario o clave incorrectas.");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl.SelectedTab = signupPage;
        }

        private void gotoLogin(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl.SelectedTab = loginPage;
        }

        private void signUpClicked(object sender, EventArgs e)
        {
            var fetched = fetchSignUpForm();
            if (b_user.createUser(fetched))
                tabControl.SelectedTab = homePage;
            else
                MessageBox.Show("No se puedo completar el registro.");
        }

        private E_User fetchSignUpForm()
        {
            E_User user = new E_User();
            user.Name = nameField.Text;
            user.LastName = lastNameField.Text;
            user.Email = emailField.Text;
            user.Password = passwordSGField.Text;
            user.IsAdmin = false;

            return user;
        }

        private void gotoProducts(object sender, EventArgs e)
        {
            if (homeTabControl.SelectedTab == productPage)
                return;

            homeTabControl.SelectedTab = productPage;
        }

        private void gotoStatistics(object sender, EventArgs e)
        {
            if (homeTabControl.SelectedTab == findProductPage)
                return;

            homeTabControl.SelectedTab = findProductPage;
        }

        private void gotoUsers(object sender, EventArgs e)
        {
            if (homeTabControl.SelectedTab == usersPage)
                return;

            homeTabControl.SelectedTab = usersPage;
        }

        private void addProductClicked(object sender, EventArgs e)
        {
            bool res = b_product.addNewProduct(getProductToAdd());
            string msg = res ? "Producto agregado exitosamente!" : "Error al agregar producto";
            MessageBox.Show(msg);
        }

        private E_Product getProductToAdd()
        {
            E_Product product = new E_Product();
            product.Name = productName.Text;
            product.Description = productDescription.Text;
            product.Price = int.Parse(productPrice.Text);

            return product;
        }

        private E_Product getProductToModify()
        {
            E_Product product = new E_Product();
            product.Name = namePC.Text;
            product.Description = descriptionPC.Text;
            product.Price = int.Parse(pricePC.Text);

            return product;
        }

        private void searchProductClicked(object sender, EventArgs e)
        {
            E_Product fetched = b_product.productByName(searchField.Text);
            if (fetched == null)
                MessageBox.Show("El producto con ese nombre no existe");
            else
            {
                foundProuct = fetched;
                fillProductCard(fetched);
            }
        }

        private void fillProductCard(E_Product product)
        {
            namePC.Text = product.Name;
            descriptionPC.Text = product.Description;
            pricePC.Text = product.Price.ToString();

            productCard.Visible = true;
        }

        private void modifyProductClicked(object sender, EventArgs e)
        {
            var fetched = getProductToModify();
            fetched.Id = foundProuct.Id;

            var res = b_product.modifyProduct(fetched);
            var msg = res ? "Producto modificado exitosamente!" : "Error al modificar producto.";
            MessageBox.Show(msg);
        }

        private void gotoAddProduct(object sender, EventArgs e)
        {
            if (productTabControl.SelectedTab == addProductPage)
                return;
            productTabControl.SelectedTab = addProductPage;
        }

        private void gotoSearchProduct(object sender, EventArgs e)
        {
            if (productTabControl.SelectedTab == searchProductPage)
                    return;
            productTabControl.SelectedTab = searchProductPage;
        }
    }
}
