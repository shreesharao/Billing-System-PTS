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
using System.Windows.Media.Animation;

using BillingSystam.Printer;
using BillingSystem.Entities;
using BillingSystem.Business;
using BillingSystem.Utility;

namespace BillingSystam
{
    /// <summary>
    /// Interaction logic for Sale.xaml
    /// </summary>
    public partial class Sale : UserControl, ITabbedWindow
    {
        public event CloseTab CloseInitiated;
        public string TabName { get; set; }
        public string TabTitle { get; set; }

        #region Private Variables

        List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
        Printer.Printer objPrinter = new Printer.Printer();
        SalesEntity objSalesEntity;

        SalesBL objSalesBL = new SalesBL();
        int errors = 0;

        bool isItemExist = false;
        int checkbocCheckedCount = 0;
        #endregion

        public Sale()
        {
            InitializeComponent();
        }

        public Sale(string name, string title)
        {
            InitializeComponent();
            TabName = name;
            TabTitle = title;


            this.DataContext = new SalesEntity();

            LoadItemTypes();

        }

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, e);
            }
        }

        private void chkBox_Checked_1(object sender, RoutedEventArgs e)
        {
            checkbocCheckedCount++;
        }

        private void chkBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            checkbocCheckedCount--;
        }

        private void btnPrintInvoice_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                int invoiceNumber = objSalesBL.GetInvoiceNumber();
                invoiceNumber = invoiceNumber + 1;

                string year = DateTime.Now.Year.ToString();
                string nextYear = DateTime.Now.AddYears(1).Year.ToString();
                string invoiceNumWithYear = invoiceNumber.ToString() + @"\" + year.Substring(2, 2) + "-" + nextYear.Substring(2, 2);

                foreach (SalesEntity entity in lstSalesEntity)
                {
                    entity.InvoiceNumber = invoiceNumber;
                    entity.InvoiceNumWithYear = invoiceNumWithYear;
                    entity.Date = DateTime.Now.ToShortDateString();
                    entity.DelNote = txtDelNote.Text.Trim();
                }

                objSalesBL.SellItems(lstSalesEntity);
                objPrinter.Print(lstSalesEntity, invoiceNumWithYear.Replace("\\", "-"));
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        private void btnAddAnotherItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                if (errors > 0)
                {
                    MessageBox.Show("Please fill all the details", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if(cmbItemType.Text=="Select")
                {
                    MessageBox.Show("Please select Item Type", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if ((isItemExist) && (checkbocCheckedCount == 0))
                    {
                        MessageBox.Show("Please select atleast one item", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    else
                    {
                        objSalesEntity = new SalesEntity();

                        List<SalesEntity> lstItems = new List<SalesEntity>();
                        lstItems = dgItems.ItemsSource as List<SalesEntity>;
                        foreach (SalesEntity entity in lstItems)
                        {
                            if (entity.Selected)
                            {
                                objSalesEntity.Count = objSalesEntity.Count + 1;
                                objSalesEntity.SerialItemName += entity.SerialNumber + "-" + entity.Itemname + ",";
                                objSalesEntity.SerialNumber += entity.SerialNumber + ",";
                                objSalesEntity.Itemname += entity.Itemname + ",";
                                objSalesEntity.UniqueNum += "'"+entity.UniqueNum + "',";
                            }
                        }

                        objSalesEntity.SerialItemName = objSalesEntity.SerialItemName.Substring(0, objSalesEntity.SerialItemName.LastIndexOf(","));
                        objSalesEntity.SerialNumber = objSalesEntity.SerialNumber.Substring(0, objSalesEntity.SerialNumber.LastIndexOf(","));
                        objSalesEntity.Itemname = objSalesEntity.Itemname.Substring(0, objSalesEntity.Itemname.LastIndexOf(","));
                        objSalesEntity.UniqueNum = objSalesEntity.UniqueNum.Substring(0, objSalesEntity.UniqueNum.LastIndexOf(","));
                        objSalesEntity.Buyer = txtBuyer.Text.Trim();
                        objSalesEntity.Address = txtAddress.Text.Trim();
                        //  objSalesEntity.UniqueNum = txtUniqueNum.Text.Trim();
                        //  objSalesEntity.SerialNumber = txtSerialNum.Text.Trim();
                        objSalesEntity.Price = txtPrice.Text.Trim();
                        //objSalesEntity.Vat = txtVAT.Text.Trim();
                        objSalesEntity.CGST = txtCGST.Text.Trim();
                        objSalesEntity.SGST = txtSGST.Text.Trim();

                        lstSalesEntity.Add(objSalesEntity);
                        MessageBox.Show("Item added successfully to the list", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);

                        //txtSerialNum.Text = "";
                        // txtUniqueNum.Clear();
                    
                        txtBuyer.IsEnabled = false;
                        txtAddress.IsEnabled = false;
                        //txtVAT.IsEnabled = false;
                        txtCGST.IsEnabled = false;
                        txtSGST.IsEnabled = false;
                        txtDelNote.IsEnabled = false;
                        btnPrintInvoice.IsEnabled = true;
                    }
                }

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

        //private void txtUniqueNum_LostFocus_1(object sender, RoutedEventArgs e)
        //{
        //    string serialNumber = objSalesBL.GetSerialNumber(txtUniqueNum.Text.Trim().ToUpper());
        //    txtSerialNum.Text = serialNumber;
        //}

        private void cmbItemType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<SalesEntity> lstSellingItems = new List<SalesEntity>();
                if (cmbItemType.SelectedValue != null)
                {
                    lstSellingItems = objSalesBL.GetItems(cmbItemType.SelectedValue.ToString());
                    rowDG.Height = GridLength.Auto;

                    if (lstSellingItems.Count > 0)
                    {
                        isItemExist = true;
                        btnAddAnotherItem.IsEnabled = true;

                        lblNoData.Visibility = Visibility.Collapsed;
                        dgItems.Visibility = Visibility.Visible;


                        dgItems.ItemsSource = lstSellingItems;


                        DoubleAnimation doubleAnimation = new DoubleAnimation();
                        doubleAnimation.From = 0;
                        doubleAnimation.To = lstSellingItems.Count * 20 + 35;
                        doubleAnimation.Duration = TimeSpan.FromSeconds(1);

                        dgItems.BeginAnimation(DataGrid.HeightProperty, doubleAnimation);
                    }
                    else
                    {
                        isItemExist = false;
                        btnAddAnotherItem.IsEnabled = false;

                        dgItems.Visibility = Visibility.Collapsed;
                        lblNoData.Visibility = Visibility.Visible;
                        lblNoData.Content = "No Data Found";
                    }
                }
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
                lstItemtypes = objSalesBL.GetItemTypes();
                cmbItemType.ItemsSource = lstItemtypes;
                cmbItemType.DisplayMemberPath = "Value";
                cmbItemType.SelectedValuePath = "Value";
                cmbItemType.SelectedValue = "Select";



            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
                MessageBox.Show("There is some problem.Please check the log file", "Alert", MessageBoxButton.OK, MessageBoxImage.Stop);
            }






        }





    }
}
