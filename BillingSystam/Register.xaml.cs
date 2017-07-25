using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

//custom namespaces
using BillingSystem.Entities;
using BillingSystem.Business;
using BillingSystem.Utility;

namespace BillingSystam
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        #region Private Variables

        UserEntity objUserEntity=new UserEntity();
        UserBL objUserBL=new UserBL();

        #endregion
        #region Window Events
        
        
        public Register()
        {
            InitializeComponent();

            //get focus to username textbox
            txtUserName.Focus();
            Keyboard.Focus(txtUserName);
        }

        private void btnRegister_Click_1(object sender, RoutedEventArgs e)
        {
            lblInvalidPassword.Visibility = Visibility.Hidden;
            lblInvalidUser.Visibility = Visibility.Hidden;
            lblInvalidConPassword.Visibility = Visibility.Hidden;

            try
            {
                if (!String.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    if (!String.IsNullOrEmpty(txtPassword.Password.Trim()))
                    {
                        if (!String.IsNullOrEmpty(txtConPassword.Password.Trim()))
                        {
                            if (txtPassword.Password != txtConPassword.Password)
                            {
                                MessageBox.Show("Password and confirm password do not match", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                                txtPassword.Focus();
                                txtPassword.Clear();
                                txtConPassword.Clear();
                            }
                            else
                            {
                                objUserEntity.UserName = txtUserName.Text.Trim();
                                objUserEntity.Password = txtPassword.Password.Trim();

                                if (objUserBL.checkUserName(objUserEntity.UserName))
                                {
                                    MessageBox.Show("user name already exist!\nplease try another user name", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                else
                                {
                                    if (objUserBL.Register(objUserEntity) > 0)
                                    {
                                        MessageBox.Show("Registration successful", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }

                                }

                            }
                        }
                        else
                        {
                            lblInvalidConPassword.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        lblInvalidPassword.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    lblInvalidUser.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop); 
            }

            
        }

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        #endregion
    }
}
