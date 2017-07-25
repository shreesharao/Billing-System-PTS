using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BillingSystem.Entities;
using System.Data;

namespace BillingSystem.Data
{
  public class SalesDL
    {
        string result = string.Empty;
        SQLiteHelper objSQLiteHelper = new SQLiteHelper();


        //public string GetSerialNumber(string uniqueNum)
        //{
        //    string query = "select SERIALNUMBER from TB_ItemInStcok where UNIQUENUMBER='" + uniqueNum + "';";
        //    object objResult=objSQLiteHelper.ExecuteScalar(query);
        //    if (objResult != DBNull.Value)
        //    {
        //        result = objResult.ToString();
        //    }
        //    return result;
        //}

        public bool SellItems(List<SalesEntity> listEntity)
        {
            bool isSucess = false;
            try
            {
                foreach (SalesEntity entity in listEntity)
                {
                    string deletequery = "update TB_ItemInStcok SET ISDELETED=1 where UNIQUENUMBER in (" + entity.UniqueNum + ")";

                    string query = "insert into TB_SoldItems(INVOICENUM,INVOCENUMWITHYEAR,BUYER,ADDRESS,SERIALNUMBER,UNIQUENUMBER,";
                    query = query + "PRICE,SDATE,VAT) values (" + entity.InvoiceNumber + ",'" + entity.InvoiceNumWithYear + "','" + entity.Buyer + "','" + entity.Address + "','" + entity.SerialNumber + "','";
                    query = query + entity.UniqueNum.Replace("'","") + "','" + entity.Price + "','" + entity.Date + "','" + entity.Vat + "');";

                    
                    int result = objSQLiteHelper.ExecuteNonQuery(query);
                    result = objSQLiteHelper.ExecuteNonQuery(deletequery);
                    if (result > 0)
                    {
                        isSucess = true;
                    }
                    else
                    {
                        isSucess = false;
                    }

                }
               
            }
            catch (Exception)
            {

                
                throw;
            }
            
            return isSucess;

        }
        public int GetInvoiceNumber()
        {
            int result = 0;
            try
            {
                string query = "select max(INVOICENUM) from TB_SoldItems";
                object temp = objSQLiteHelper.ExecuteScalar(query);
                if (temp == DBNull.Value)
                {
                    result = 0;
                }
                else
                {
                    result = Convert.ToInt32(temp);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           
                return result;
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
                lstItemtypes.Add(0,"Select");

            }
            catch (Exception ex)
            {

                throw;
            }

            return lstItemtypes;
        }

        public List<SalesEntity> GetItems(string itemType)
        {
            List<SalesEntity> lstSalesEntity = new List<SalesEntity>();
            

            try
            {
                objSQLiteHelper = new SQLiteHelper();
                string query = "select SERIALNUMBER,UNIQUENUMBER,ITEM,PRICE from TB_ItemInStcok where ISDELETED=0 and ITEMTYPE ='" + itemType + "';";
                DataSet ds = objSQLiteHelper.ExecuteDataset(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SalesEntity objSalesEntity = new SalesEntity();

                        objSalesEntity.Selected = false;
                        objSalesEntity.SerialNumber = row[0].ToString();
                        objSalesEntity.UniqueNum = row[1].ToString();
                        objSalesEntity.Itemname = row[2].ToString();
                        objSalesEntity.Price = row[3].ToString();

                        lstSalesEntity.Add(objSalesEntity);

                    }
                } 
            }
            catch (Exception)
            {
                
                throw;
            }
            return lstSalesEntity;
        }

    }
}
