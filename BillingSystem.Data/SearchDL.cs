using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BillingSystem.Utility;
using BillingSystem.Entities;

namespace BillingSystem.Data
{
    public class SearchDL
    {
        SQLiteHelper objSQLiteHelper;


        public Dictionary<int, string> GetPurchasers()
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

        public DataTable Search(SearchEntity entity)
        {
            DataTable objDT = new DataTable();
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("select ID,PURCHASERNAME,ITEMTYPE,ITEM,SERIALNUMBER,UNIQUENUMBER,PRICE,PDATE from TB_ItemInStcok where ISDELETED =0 ");
                if (!entity.PurchasedFrom.Equals("Select"))
                {
                    queryBuilder.Append(" and PURCHASERNAME='" + entity.PurchasedFrom + "'");
                }
                if (!entity.ItemType.Equals("Select"))
                {
                    queryBuilder.Append(" and ITEMTYPE='" + entity.ItemType + "'");
                }
                if (!string.IsNullOrEmpty(entity.SerialNumber))
                {
                    queryBuilder.Append(" and SERIALNUMBER='" + entity.SerialNumber + "'");
                }
                
                if (!string.IsNullOrEmpty(entity.UniqueNum))
                {
                    queryBuilder.Append(" and UNIQUENUMBER='" + entity.UniqueNum + "'");
                }

                if ((entity.Price > 0))
                {
                    queryBuilder.Append(" and PRICE=" + entity.Price);
                }
                if (entity.FromDate !=0)
                {

                    queryBuilder.Append(" and PDATE between " + entity.FromDate);
                }
                if (entity.ToDate != 0)
                {
                    queryBuilder.Append(" and " + entity.ToDate);
                }

                string query = queryBuilder.ToString();
               

                objDT = objSQLiteHelper.ExecuteDataset(query).Tables[0];

                
                

                //for the id column -show the item in order
                int rowNum = 0;
                foreach (DataRow row in objDT.Rows)
                {
                    row["ID"] = ++rowNum;
                }

                objDT.Columns["PURCHASERNAME"].ColumnName= "PURCHASER NAME";
                objDT.Columns["ITEMTYPE"].ColumnName= "ITEM TYPE";
                objDT.Columns["ITEM"].ColumnName = "ITEM NAME";
                objDT.Columns["SERIALNUMBER"].ColumnName = "SERIAL NUMBER";
                objDT.Columns["UNIQUENUMBER"].ColumnName = "PTS NUMBER";
                objDT.Columns["PRICE"].ColumnName = "PRICE";
                
                //for converting purchased date from long value to datetime object
                objDT.Columns.Add("PURCHASED DATE");
                foreach (DataRow row in objDT.Rows)
                {
                    Int64 value = Convert.ToInt64(row["PDATE"]);
                    
                    row["PURCHASED DATE"] = new DateTime(value);
                }
                objDT.Columns.Remove("PDATE");

               
                
                objDT.AcceptChanges();
            }
            catch (Exception)
            {
                
                throw;
            }


            return objDT;
        }

        public DataTable SearchSold(SearchEntity entity)
        {
            DataSet objDS = new DataSet();
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("select ID,INVOICENUM,INVOCENUMWITHYEAR,BUYER,ADDRESS,SERIALNUMBER,UNIQUENUMBER as 'PTS NUMBER',PRICE,SDATE as 'SOLD DATE',VAT as 'VAT%' from TB_SoldItems where 1=1 ");


                if (entity.InvoiceNum !=0)
                {
                    queryBuilder.Append(" and INVOICENUM=" + entity.InvoiceNum);
                }

                //if (!string.IsNullOrEmpty(entity.ItemType))
                //{
                //    queryBuilder.Append(" and ITEMTYPE='" + entity.ItemType + "'");
                //}


                string query = queryBuilder.ToString();
                objDS = objSQLiteHelper.ExecuteDataset(query);

                //for the id column -show the item in order
                int rowNum = 0;
                foreach (DataRow row in objDS.Tables[0].Rows)
                {
                    row["ID"] = ++rowNum;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            

            return objDS.Tables[0];
        }

        public int Delete(SearchEntity entity)
        {
            int rows=0;
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("update TB_ItemInStcok SET ISDELETED=1 where ISDELETED =0 ");
                if (!entity.PurchasedFrom.Equals("Select"))
                {
                    queryBuilder.Append(" and PURCHASERNAME='" + entity.PurchasedFrom + "'");
                }
                if (!entity.ItemType.Equals("Select"))
                {
                    queryBuilder.Append(" and ITEMTYPE='" + entity.ItemType + "'");
                }
                if (!string.IsNullOrEmpty(entity.SerialNumber))
                {
                    queryBuilder.Append(" and SERIALNUMBER='" + entity.SerialNumber + "'");
                }

                if (!string.IsNullOrEmpty(entity.UniqueNum))
                {
                    queryBuilder.Append(" and UNIQUENUMBER='" + entity.UniqueNum + "'");
                }
                if (entity.FromDate != 0)
                {

                    queryBuilder.Append(" and PDATE between " + entity.FromDate);
                }
                if (entity.ToDate != 0)
                {
                    queryBuilder.Append(" and " + entity.ToDate);
                }

                string query = queryBuilder.ToString();

                rows = objSQLiteHelper.ExecuteNonQuery(query);
            }
            catch (Exception)
            {
                throw;
            }
            return rows;
        }
    }
}
