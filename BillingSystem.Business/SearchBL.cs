using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


using BillingSystem.Data;
using BillingSystem.Entities;

namespace BillingSystem.Business
{
   public class SearchBL
    {
       SearchDL objSearchDL = new SearchDL();

        public Dictionary<int, string> GetPurchasers()
        {
            return objSearchDL.GetPurchasers();
            
        }

        public Dictionary<int, string> GetItemTypes()
        {
            return objSearchDL.GetItemTypes();
        }

        public DataTable Search(SearchEntity entity)
        {
            return objSearchDL.Search(entity);
        }
        public DataTable SearchSold(SearchEntity entity)
        {
            return objSearchDL.SearchSold(entity);
        }
        public int Delete(SearchEntity entity)
        {
            return objSearchDL.Delete(entity);
        }

       
    }
}
