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

using System.Data;
//custom namespaces
using BillingSystem.Entities;
using BillingSystem.Business;
using BillingSystem.Utility;

namespace BillingSystam
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl, ITabbedWindow
    {
        public event CloseTab CloseInitiated;
        public string TabName { get; set; }
        public string TabTitle { get; set; }

        #region Private variables
        SearchEntity objSearchEntity = new SearchEntity();
        SearchBL objSearchBL = new SearchBL();
        #endregion

        public Search()
        {
            InitializeComponent();
        }
        public Search(string name, string title)
        {
            InitializeComponent();
            TabName = name;
            TabTitle = title;

            LoadPurchasers();

            LoadItemTypes();
        }

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, e);
            }
        }

        private void btnSearchInStock_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable objDT = new DataTable();
                SearchEntity objSearchEntity = PrepareSearchEntity();


                objDT = objSearchBL.Search(objSearchEntity);

                if (objDT.Rows.Count == 0)
                {
                    MessageBox.Show("No Data Found-Search Again", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    gridResult.DataContext = objDT.DefaultView;


                    gridInStock.Visibility = Visibility.Collapsed;
                    gridResult.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        private void btnSearchSold_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int result = 0;
                //if (string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                //{
                //    MessageBox.Show("Enter invoice number to search", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                if ((!string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim())) && (!int.TryParse(txtInvoiceNumber.Text.Trim(), out result)))
                {
                    MessageBox.Show("Invoice number should be a number", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    DataTable objDT = new DataTable();
                    SearchEntity objSearchEntity = new SearchEntity();

                    if (!string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                    {
                        objSearchEntity.InvoiceNum = Convert.ToInt32(txtInvoiceNumber.Text.Trim());
                    }

                    objDT = objSearchBL.SearchSold(objSearchEntity);
                    if (objDT.Rows.Count == 0)
                    {
                        MessageBox.Show("No Data Found-Search Again", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {

                        gridResult.DataContext = objDT.DefaultView;


                        gridSold.Visibility = Visibility.Collapsed;
                        gridResult.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }
        private void btnDeleteInStock_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                SearchEntity objSearchEntity = PrepareSearchEntity();
                int rows = objSearchBL.Delete(objSearchEntity);
                //display the number of rows deleted
                MessageBox.Show(string.Format("{0} {1}",rows,"items deleted"), "Alert", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        private void cmbSelectType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            int index = Convert.ToInt32(cmbSelectType.SelectedIndex.ToString());
            switch (index)
            {
                case 1: gridInStock.Visibility = Visibility.Visible;
                    gridSold.Visibility = Visibility.Collapsed;
                    gridResult.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    gridSold.Visibility = Visibility.Visible;
                    gridInStock.Visibility = Visibility.Collapsed;
                    gridResult.Visibility = Visibility.Collapsed;
                    break;
            }

        }

        

        private void LoadPurchasers()
        {
            try
            {
                Dictionary<int, string> lstPurchaser = new Dictionary<int, string>();
                lstPurchaser = objSearchBL.GetPurchasers();
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
                lstItemtypes = objSearchBL.GetItemTypes();
                cmbItem.ItemsSource = lstItemtypes;
                cmbItem.DisplayMemberPath = "Value";
                cmbItem.SelectedValuePath = "Key";
                cmbItem.SelectedValue = "0";
                //sold grid

                //cmbSoldItem.ItemsSource = lstItemtypes;
                //cmbSoldItem.DisplayMemberPath = "Value";
                //cmbSoldItem.SelectedValuePath = "Key";
                //cmbSoldItem.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
            }






        }

        private SearchEntity PrepareSearchEntity()
        {
            SearchEntity objSearchEntity = new SearchEntity();
            objSearchEntity.PurchasedFrom = cmbSeller.Text;
            objSearchEntity.ItemType = cmbItem.Text;
            objSearchEntity.SerialNumber = txtSerailNum.Text.Trim();
            objSearchEntity.UniqueNum = txtUnique.Text.Trim();
            int result=0;
            if (!string.IsNullOrEmpty(txtPrice.Text.Trim()) && (int.TryParse(txtPrice.Text.Trim(), out result)))
            {
                objSearchEntity.Price = result;
            }

            if (DatePickerFrom.SelectedDate != null && DatePickerTo.SelectedDate != null)
            {
                objSearchEntity.FromDate = DatePickerFrom.SelectedDate.Value.Ticks;
                objSearchEntity.ToDate = DatePickerTo.SelectedDate.Value.Ticks;
            }
            return objSearchEntity;
        }

       


    }
}
