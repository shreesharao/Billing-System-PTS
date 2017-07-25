using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BillingSystem.Data;
using BillingSystem.Entities;
namespace BillingSystem.Business
{
  public class SalesBL
    {
      SalesDL objSalesDL = new SalesDL();
      SalesEntity objSalesEntity = new SalesEntity();
      int result = 0;

      //public string GetSerialNumber(string uniqueNum)
      //{
      //    return objSalesDL.GetSerialNumber(uniqueNum);
      //}

      public int GetInvoiceNumber()
      {
          return objSalesDL.GetInvoiceNumber();
      }

      public bool SellItems(List<SalesEntity> listEntity)
        {
          return objSalesDL.SellItems(listEntity);
        }

      public Dictionary<int, string> GetItemTypes()
      {
          return objSalesDL.GetItemTypes();
      }

      public List<SalesEntity> GetItems(string itemType)
      {
          return objSalesDL.GetItems(itemType);
      }
    }
}
