using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace BillingSystem.Entities
{
   public class SalesEntity:IDataErrorInfo
    {
       Regex objRegex = new Regex("^[a-zA-Z]+");
       string result = string.Empty;
       private int invoiceNumber;

        public int InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; }
        }

        private string invoiceNumWithYear;

        public string InvoiceNumWithYear
        {
            get { return invoiceNumWithYear; }
            set { invoiceNumWithYear = value; }
        }
        private string buyer;

        public string Buyer
        {
            get { return buyer; }
            set { buyer = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string uniqueNum;

        public string UniqueNum
        {
            get { return uniqueNum; }
            set { uniqueNum = value; }
        }
        private string serialNumber;

        public string SerialNumber
        {
            get { return serialNumber; }
            set { serialNumber = value; }
        }
        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; }
        }
        private string vat;

        public string Vat
        {
            get { return vat; }
            set { vat = value; }
        }

        private string cgst;

        public string CGST
        {
            get { return cgst; }
            set { cgst = value; }
        }

        private string sgst;

        public string SGST
        {
            get { return sgst; }
            set { sgst = value; }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        private string itemname;

        public string Itemname
        {
            get { return itemname; }
            set { itemname = value; }
        }

        private bool selected;

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        private string delNote;

        public string DelNote
        {
            get { return delNote; }
            set { delNote = value; }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private string serialItemName;

        public string SerialItemName
        {
            get { return serialItemName; }
            set { serialItemName = value; }
        }

        public string this[string columnName]
        {
            get
            {
                result = string.Empty;
                switch (columnName)
                {
                    case "Buyer":
                        if (string.IsNullOrEmpty(Buyer))
                        {
                            result = "Buyer can not be empty";
                        }
                        break;

                    case "UniqueNum":
                        if (string.IsNullOrEmpty(UniqueNum))
                        {
                            result = "Unique Number can not be empty";
                        }
                        
                        break;

                    case "Price":
                        if (string.IsNullOrEmpty(Price))
                        {
                            result = "Item Price can not be empty";
                        }
                        else if (objRegex.IsMatch(Price))
                        {
                            result = "Price can only be in digits";
                        }
                        break;
                    case "Vat":
                        if (string.IsNullOrEmpty(Vat))
                        {
                            result = "VAT can not be empty";
                        }
                        else if (objRegex.IsMatch(Vat))
                        {
                            result = "VAT can only be in digits";
                        }
                        break;

                    case "CGST":
                        if (string.IsNullOrEmpty(CGST))
                        {
                            result = "CGST can not be empty";
                        }
                        else if (objRegex.IsMatch(CGST))
                        {
                            result = "CGST can only be in digits";
                        }
                        break;

                    case "SGST":
                        if (string.IsNullOrEmpty(SGST))
                        {
                            result = "SGST can not be empty";
                        }
                        else if (objRegex.IsMatch(SGST))
                        {
                            result = "SGST can only be in digits";
                        }
                        break;

                }
                return result;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
