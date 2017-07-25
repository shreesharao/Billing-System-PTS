using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Text.RegularExpressions;

namespace BillingSystem.Entities
{
    public class PurchaseEntity:IDataErrorInfo
    {
        Regex objRegex = new Regex("^[a-zA-Z]+");
        private string result { get; set; }

        private int itemId;

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }
        private bool isDeleted;

        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        private string purchaserName;

        public string PurchaserName
        {
            get { return purchaserName; }
            set { purchaserName = value; }
        }

        private int puchaserId;

        public int PuchaserId
        {
            get { return puchaserId; }
            set { puchaserId = value; }
        }

        private string itemType;

        public string ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }

        private string itemname;

        public string Itemname
        {
            get { return itemname; }
            set { itemname = value; }
        }

        private int itemTypeId;

        public int ItemTypeId
        {
            get { return itemTypeId; }
            set { itemTypeId = value; }
        }

        private string serialNumber;

        public string SerialNumber
        {
            get { return serialNumber; }
            set { serialNumber = value; }
        }
        private string uniqueNumber;

        public string UniqueNumber
        {
            get { return uniqueNumber; }
            set { uniqueNumber = value; }
        }
        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; }
        }
        private long date;

        public long Date
        {
            get { return date; }
            set { date = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }


        public string this[string columnName]
        {
            get
            {
                result = string.Empty;
                switch (columnName)
                {
                    case "SerialNumber":
                        if (string.IsNullOrEmpty(SerialNumber))
                        {
                            result = "Serial Number can not be empty";
                        }
                        break;

                    case "Price":
                        if (string.IsNullOrEmpty(Price))
                        {
                            result = "Price can not be empty";
                        }
                        else if(objRegex.IsMatch(Price))
                        {
                            result = "Price can only be in digits";
                        }
                        break;

                    case "PurchaserName":
                        if(string.IsNullOrEmpty(PurchaserName) ||PurchaserName.Equals("Select"))
                        {
                            result = "Please select Purchaser Name";
                        }
                        break;
                    case "ItemType":
                        if (string.IsNullOrEmpty(ItemType) || ItemType.Equals("Select"))
                        {
                            result = "Please select Item Type";
                        }
                        break;

                    case "Itemname":
                        if (string.IsNullOrEmpty(Itemname))
                        {
                            result = "Item can not be empty ";
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
