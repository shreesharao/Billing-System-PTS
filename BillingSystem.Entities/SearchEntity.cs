using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Entities
{
  public  class SearchEntity
    {
        private long fromDate;

        public long FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }
        private long toDate;

        public long ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }
        private string uniqueNum;

        public string UniqueNum
        {
            get { return uniqueNum; }
            set { uniqueNum = value; }
        }
        private string purchasedFrom;

        public string PurchasedFrom
        {
            get { return purchasedFrom; }
            set { purchasedFrom = value; }
        }
        private string itemType;

        public string ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }

        private int invoiceNum;

        public int InvoiceNum
        {
            get { return invoiceNum; }
            set { invoiceNum = value; }
        }

        private string serialNumber;

        public string SerialNumber
        {
            get { return serialNumber; }
            set { serialNumber = value; }
        }

        private int price;

        public int Price
        {
            get { return price; }
            set { price = value; }
        }


    }
}
