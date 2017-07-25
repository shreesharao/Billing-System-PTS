using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BillingSystem.Entities;
using BillingSystem.Data;

namespace BillingSystem.Business
{
    public class PurchaseBL
    {
        PurchaseDL objPurchaseDL = new PurchaseDL();
        public int AddPurchaseDetails(PurchaseEntity entity)
        {
          return  objPurchaseDL.AddPurchaseDetails(entity);
        }
        public int AddItem(PurchaseEntity entity)
        {
            return objPurchaseDL.AddItem(entity);
        }

        public int AddSeller(PurchaseEntity entity)
        {
            return objPurchaseDL.AddSeller(entity);
        }

        public Dictionary<int, string> GetPurchasers()
        {
            return  objPurchaseDL.GetPurchasers();

        }

        public Dictionary<int, string> GetItemTypes()
        {
            return objPurchaseDL.GetItemTypes();            
        }

        public int getLastUniqueNum()
        {

            return objPurchaseDL.getLastUniqueNum();
        }
    }
}
