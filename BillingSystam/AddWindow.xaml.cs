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

using BillingSystem.Entities;
using BillingSystem.Business;
using BillingSystem.Utility;

namespace BillingSystam
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        #region Private variables
        PurchaseEntity objPurchaseEntity = new PurchaseEntity();
        PurchaseBL objPurchaseBL = new PurchaseBL();
        #endregion

        public AddWindow()
        {
            InitializeComponent();
        }

        public AddWindow(string Window)
        {
            InitializeComponent();
            switch (Window)
            {
                case "Item": gridItemType.Visibility = Visibility.Visible;
                    gridSeller.Visibility = Visibility.Collapsed;
                    txtItemType.Focus();
                    Keyboard.Focus(txtItemType);
                    break;

                case "Seller": gridSeller.Visibility = Visibility.Visible;
                    gridItemType.Visibility = Visibility.Collapsed;
                    txtSeller.Focus();
                    Keyboard.Focus(txtSeller);
                    break;
            }
        }

        private void btnAddItem_Click_1(object sender, RoutedEventArgs e)
        {
            lblInvalidItem.Visibility=Visibility.Hidden;
            try
            {
                if (!string.IsNullOrEmpty(txtItemType.Text.Trim()))
                {
                    objPurchaseEntity.ItemType = txtItemType.Text.Trim();

                    if (objPurchaseBL.AddItem(objPurchaseEntity) > 0)
                    {
                        MessageBox.Show("Item Type added successfully", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    

                }
                else
                {
                    lblInvalidItem.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            
        }

        private void btnAddSeller_Click_1(object sender, RoutedEventArgs e)
        {
            lblInvalidSeller.Visibility = Visibility.Hidden;
            try
            {
                if (!string.IsNullOrEmpty(txtSeller.Text.Trim()))
                {
                    objPurchaseEntity.PurchaserName = txtSeller.Text.Trim();
                    objPurchaseEntity.Address = txtAddress.Text.Trim();
                    if (objPurchaseBL.AddSeller(objPurchaseEntity) > 0)
                    {
                        MessageBox.Show("Seller added successfully", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    lblInvalidSeller.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
           
        }
    }
}
