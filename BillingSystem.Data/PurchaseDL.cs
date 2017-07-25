using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

using BillingSystem.Entities;
using BillingSystem.Utility;
using System.Data;

namespace BillingSystem.Data
{
    public class PurchaseDL
    {
        SQLiteHelper objSQLiteHelper = null;
        int result = 0;
       
        public int AddPurchaseDetails(PurchaseEntity entity)
        {
            objSQLiteHelper = new SQLiteHelper();
            try
            {
                string query = "insert into TB_ItemInStcok(ITEMID,PURCHASERNAME,ITEMTYPE,ITEM,SERIALNUMBER,UNIQUENUMBER,PRICE,PDATE,ISDELETED) values (" + entity.ItemId + ",'" + entity.PurchaserName + "','" + entity.ItemType + "','" + entity.Itemname + "','" + entity.SerialNumber + "','" + entity.UniqueNumber + "','" + entity.Price + "','" + entity.Date + "',"+(entity.IsDeleted == false ? 0:1)+");";
                result = objSQLiteHelper.ExecuteNonQuery(query);

            }
            catch (Exception ex)
            {
                throw;

            }
            return result;
        }

        public int AddItem(PurchaseEntity entity)
        {
            objSQLiteHelper = new SQLiteHelper();
            try
            {
                string query = "insert into TB_Items(ITEMTYPE) values ('" + entity.ItemType + "');";
                result = objSQLiteHelper.ExecuteNonQuery(query);
               
            }
            catch (Exception ex)
            {
                throw;
                
            }
            return result;
        }

        public int AddSeller(PurchaseEntity entity)
        {
            objSQLiteHelper = new SQLiteHelper();
            try
            {
                string query = "insert into TB_Seller(SELLER,ADDRESS) values ('" + entity.PurchaserName + "','" + entity.Address + "');";
                result = objSQLiteHelper.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw;
                
            }
            
            return result;
            
        }

        public Dictionary<int,string>  GetPurchasers()
        {
           
            Dictionary<int, string> lstPurchaser = new Dictionary<int, string>();
            objSQLiteHelper = new SQLiteHelper();

            try
            {
                string query = "select ID,SELLER from TB_Seller";
                DataSet ds = objSQLiteHelper.ExecuteDataset(query);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lstPurchaser.Add(Convert.ToInt32(row[0]), row[1].ToString());
                }

                lstPurchaser.Add(0, "Select");
            }
            catch (Exception ex)
            {

                throw;
            }
            
           
            return lstPurchaser;
        }

        public Dictionary<int, string> GetItemTypes()
        {
            Dictionary<int, string> lstItemtypes = new Dictionary<int, string>();
            objSQLiteHelper = new SQLiteHelper();

            try
            {
                string query = "select ID,ITEMTYPE from TB_Items";
                DataSet ds = objSQLiteHelper.ExecuteDataset(query);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lstItemtypes.Add(Convert.ToInt32(row[0]), row[1].ToString());
                }
                lstItemtypes.Add(0, "Select");
               
            }
            catch (Exception ex)
            {
                throw;              
            }
           
            return lstItemtypes;
        }
        public int getLastUniqueNum()
        {
            
            int uniqueNum = 0;
            objSQLiteHelper = new SQLiteHelper();
            try
            {
                string query = "select max(ITEMID) from TB_ItemInStcok";
                var temp = objSQLiteHelper.ExecuteScalar(query);
                if (DBNull.Value == temp)
                {
                    uniqueNum = 0;
                }
                else
                {
                    uniqueNum = Convert.ToInt32(temp);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return uniqueNum;
        }
    }
}
