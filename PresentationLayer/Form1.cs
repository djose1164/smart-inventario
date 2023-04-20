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
                setUpCurrentUser(user);
                tabControl.SelectedTab = homePage;
            }
            else
                MessageBox.Show("Usuario o clave incorrectas.");
        }

        private void setUpCurrentUser(E_User user)
        {
            instance.fillUser(user);
            var currentUser = instance.User;
            if (currentUser.IsAdmin)
            {
                groupBox2.Visible = true;
                productControlPanel.Visible = true;
            }
            setUserInfoInPane();
        }

        private void setUserInfoInPane()
        {
            var currentUser = instance.User;
            userName.Text = currentUser.Name;
            userSurname.Text = currentUser.LastName;
            userEmail.Text = currentUser.Email;
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
            {
                tabControl.SelectedTab = homePage;
                setUpCurrentUser(fetched);
            }
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
            if (productTabControl.SelectedTab == statisticsProductPage)
                return;

            productTabControl.SelectedTab = statisticsProductPage;
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
            product.Stocks = int.Parse(stocksField.Text);
            return product;
        }

        private E_Product getProductToModify()
        {
            E_Product product = new E_Product();
            product.Name = namePC.Text;
            product.Description = descriptionPC.Text;
            product.Price = int.Parse(pricePC.Text);
            product.Stocks = int.Parse(stocksPC.Text);
            product.Id = foundProuct.Id;
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

        private void searchStatisticsClicked(object sender, EventArgs e)
        {
            string arg = searchStatisticsField.Text;
            var fetched = b_product.productByName(arg);

            if (fetched == null)
            {
                MessageBox.Show("El producto con ese nombre no existe");
                return;
            }

            inStockLbl.Text = fetched.Stocks.ToString();
            soldQuantityLbl.Text = fetched.SoldQuantity.ToString();
            statisticsCard.Visible = true;
        }

        private void fillProductCard(E_Product product)
        {
            namePC.Text = product.Name;
            descriptionPC.Text = product.Description;
            pricePC.Text = product.Price.ToString();
            stocksPC.Text = product.Stocks.ToString();

            productCard.Visible = true;
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

        private void buyProduct(object sender, EventArgs e)
        {
            bool res = b_product.processTransaction(foundProuct);
            var msg = res ? "Compra existosa!" : "Error al realizar transaccion.";
            MessageBox.Show(msg);
            if (res)
                stocksPC.Text = (int.Parse(stocksPC.Text)-1).ToString();
        }

        private void acceptClicked(object sender, EventArgs e)
        {
            var res = b_user.updateUser(getUserToUpdate());
            var msg = res ? "Usuario actualizado exitosamente!" : "Error al actualizar usuario";
            MessageBox.Show(msg);
        }

        private E_User getUserToUpdate()
        {
            var usr = new E_User();
            usr.Name = nameCF.Text;
            usr.LastName = surnameCF.Text;
            usr.Email = emailCF.Text;
            usr.IsAdmin = dropdown.selectedValue == "Administrador";
            return usr;
        }

        private void deleteClicked(object sender, EventArgs e)
        {
            var res = b_product.deleteProduct(foundProuct);
            var msg = res ? "Producto eliminado con exito!" : "Error al eliminar producto.";
            MessageBox.Show(msg);
            if (res)
                productCard.Visible = false;
        }

        private void searchUserClicked(object sender, EventArgs e)
        {
            var arg = searchUserField.text;
            var toFind = new E_User();
            toFind.Email = arg;
            var fetched = b_user.getByEmail(toFind);

            if (fetched == null)
            {
                MessageBox.Show("Usuario no existe.");
                if (userCard.Visible)
                    userCard.Visible = false;
                return;
            }

            nameCF.Text = fetched.Name;
            surnameCF.Text = fetched.LastName;
            emailCF.Text = fetched.Email;
            userCard.Visible = true;
            dropdown.selectedIndex = Convert.ToInt32(fetched.IsAdmin);
        }

        private void modifyProductClicked(object sender, EventArgs e)
        {
            var res = b_product.modifyProduct(getProductToModify(), true);
            var msg = res ? "Producto modificado exitosamente" : "Error al modificar producto";
            MessageBox.Show(msg);
        }
    }
}
