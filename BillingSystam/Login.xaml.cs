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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


//custom namespaces
using BillingSystem.Entities;
using BillingSystem.Business;
using BillingSystem.Utility;

namespace BillingSystam
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        #region private variables

        UserEntity objUserEntity = new UserEntity();
        UserBL objuserBL = new UserBL();
        
        #endregion

        #region Window events
        
        public Login()
        {
            InitializeComponent();


            //Logo animation
            Storyboard objStoryBoard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0,1.0);
            imgLogo.RenderTransformOrigin = new Point(.5,.5);
            imgLogo.RenderTransform = scale;

            DoubleAnimation logoAnimation = new DoubleAnimation();
            logoAnimation.Duration = TimeSpan.FromSeconds(2);
            logoAnimation.From = 0;
            logoAnimation.To = 2;
            logoAnimation.AutoReverse = true;
            logoAnimation.RepeatBehavior = RepeatBehavior.Forever;

            objStoryBoard.Children.Add(logoAnimation);

            Storyboard.SetTargetProperty(logoAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(logoAnimation, imgLogo);

            objStoryBoard.Begin();


            //Logo animation ends

            //get focus to username textbox
            txtUserName.Focus();
            Keyboard.Focus(txtUserName);
        }

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            lblInvalidPassword.Visibility = Visibility.Hidden;
            lblInvalidUser.Visibility = Visibility.Hidden;
            try
            {
                if (!String.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    if (!String.IsNullOrEmpty(txtPassword.Password.Trim()))
                    {
                        objUserEntity.UserName = txtUserName.Text.Trim();
                        objUserEntity.Password = txtPassword.Password.Trim();

                        if (objuserBL.Login(objUserEntity))
                        {
                            MainWindow objMainWindow = new MainWindow(objUserEntity.UserName);
                            objMainWindow.Visibility = Visibility.Visible;
                            this.Close();



                        }
                        else
                        {
                            MessageBox.Show("Either username or password is incorrect", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
                            //txtPassword.Clear();
                            txtUserName.Focus();
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

        private void btnRegister_Click_1(object sender, RoutedEventArgs e)
        {
            Register objRegister = new Register();
            objRegister.ShowDialog();
        }

        #endregion
    }
}
