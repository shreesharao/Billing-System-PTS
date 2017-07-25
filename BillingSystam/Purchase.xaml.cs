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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ComponentModel;

//custom namespaces
using BillingSystem.Entities;
using BillingSystem.Business;
using BillingSystem.Utility;

namespace BillingSystam
{
    /// <summary>
    /// Interaction logic for Purchase.xaml
    /// </summary>
    public partial class Purchase : UserControl,ITabbedWindow
    {
        #region Private variables
        PurchaseEntity objPurchaseEntity = new PurchaseEntity();
        PurchaseBL objPurchaseBL = new PurchaseBL();
        int errors = 0;
        string result = string.Empty;
        int uniqueNum = 0;
        #endregion

        public event CloseTab CloseInitiated;
        public string TabName { get; set; }
        public string TabTitle { get; set; }
        
        //private string serialNumber;
        //public string SerialNumber
        //{
        //    get { return serialNumber; }
        //    set { serialNumber = value; }
        //}
        #region Constructor
        
        
        public Purchase()
        {
            InitializeComponent();
            
        }

        public Purchase(string name,string title)
        {
            InitializeComponent();
            TabName=name;
            TabTitle = title;


          this.DataContext = new PurchaseEntity(); 
        


            //method to load purchasers
            LoadPurchasers();

            //method to load Item type
            LoadItemTypes();
         
        }

       
        #endregion
        #region Window Events
        
        
        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, e);
            }

        }

        private void btnAddItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddWindow objAddWindow = new AddWindow("Item");
            objAddWindow.ShowDialog();
            LoadItemTypes();   
            
        }

        private void btnAddSeller_Click_1(object sender, RoutedEventArgs e)
        {
            AddWindow objAddWindow = new AddWindow("Seller");
            objAddWindow.ShowDialog();
            LoadPurchasers();
        }

        private void btnAddPurchaseDetail_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (errors > 0)
                {
                    MessageBox.Show("Please fill all the details", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    objPurchaseEntity.ItemId = Convert.ToInt32(uniqueNum);
                    objPurchaseEntity.PurchaserName = cmbSeller.Text;
                    objPurchaseEntity.ItemType = cmbItem.Text;
                    objPurchaseEntity.Itemname = txtItem.Text;
                    objPurchaseEntity.SerialNumber = txtSerailNum.Text.Trim();
                    objPurchaseEntity.UniqueNumber = txtUnique.Text.Trim();
                    objPurchaseEntity.Price = txtPrice.Text.Trim();
                    objPurchaseEntity.Date = DateTime.Now.Ticks;
                    objPurchaseEntity.IsDeleted = false;

                    if (objPurchaseBL.AddPurchaseDetails(objPurchaseEntity) > 0)
                    {
                        MessageBox.Show("Item added successfully\nPlease note PTS number is:" + objPurchaseEntity.UniqueNumber, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadPurchasers();
                        LoadItemTypes();
                        txtSerailNum.Clear();
                        txtUnique.Text = "";
                        txtPrice.Clear();
                        txtItem.Clear();
                    }
                }
            }
            catch (Exception ex)
            {

                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop); 
               
            }
            
            
        }
        #endregion

        #region Private Methods

        private void LoadPurchasers()
        {
            try
            {
                Dictionary<int, string> lstPurchaser = new Dictionary<int, string>();
                lstPurchaser = objPurchaseBL.GetPurchasers();
                cmbSeller.ItemsSource = lstPurchaser;
                cmbSeller.DisplayMemberPath = "Value";
                cmbSeller.SelectedValuePath = "Key";
                cmbSeller.SelectedValue = "0";
            }
            catch (Exception ex)
            {

                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop); 
               
            }
           
        }

        private void LoadItemTypes()
        {
            try
            {
                Dictionary<int, string> lstItemtypes = new Dictionary<int, string>();
                lstItemtypes = objPurchaseBL.GetItemTypes();
                cmbItem.ItemsSource = lstItemtypes;
                cmbItem.DisplayMemberPath = "Value";
                cmbItem.SelectedValuePath = "Key";
                cmbItem.SelectedValue = "0";
            }
            catch (Exception ex)
            {

                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);

            }
           
        }
        #endregion

        private void txtSerailNum_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
               
                uniqueNum = objPurchaseBL.getLastUniqueNum();
                uniqueNum++;
                //if (string.IsNullOrEmpty(uniqueNum))
                //{
                //    uniqueNum = "0";
                //}

                int year = DateTime.Now.Year;
                string month = DateTime.Now.ToString("MM");

                //write the logic to get part of unique number here
                //assign that to  txtUnique.Text
                txtUnique.Text = string.Format("{0}{1}{2}{3}", "PTS", year.ToString(), month, uniqueNum);
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                errors++;
            }
            else
            {
                errors--;
            }
            e.Handled = true;
        }

        
        
    }
}
