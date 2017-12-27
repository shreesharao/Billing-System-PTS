using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

using BillingSystem.Utility;

namespace BillingSystem.Data
{
    public class SQLiteHelper
    {
        private static string databaseDirectory = @"Database";
        private static string databaseFile = "BillingSystem.sqlite";
        SQLiteConnection objSQLiteConnection = null;

        public SQLiteHelper()
        {
       //  CreateTable();
            if (!Directory.Exists(databaseDirectory))
            {
                Directory.CreateDirectory(databaseDirectory);

            }
            //if(File.Exists(Path.GetFullPath(databaseDirectory) +"\\" +databaseFile)
        }


        public static String ConnectionString
        {
            get
            {
                return "Data Source=" + Path.GetFullPath(databaseDirectory) + "\\" + databaseFile + "; Vesrion=3;";
            }
        }

        public SQLiteConnection GetConnection()
        {
            try
            {
                objSQLiteConnection = new SQLiteConnection(ConnectionString);

                if (objSQLiteConnection.State == ConnectionState.Closed)
                {
                    objSQLiteConnection.Open();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return objSQLiteConnection;

        }
        public void CloseConnection()
        {
            try
            {
                if (objSQLiteConnection.State == ConnectionState.Open)
                {
                    objSQLiteConnection.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public object ExecuteScalar(string query)
        {
            object result = 0;
            try
            {
                SQLiteCommand objCmd = new SQLiteCommand(query, GetConnection());
                result = objCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        public DataSet ExecuteDataset(string query)
        {
            DataSet dsResult = new DataSet();

            try
            {
                SQLiteDataAdapter objDA = new SQLiteDataAdapter(query, GetConnection());
                objDA.Fill(dsResult);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                CloseConnection();
            }


            return dsResult;
        }

        public int ExecuteNonQuery(string query)
        {
            int result = 0;
            try
            {
                SQLiteCommand objCmd = new SQLiteCommand(query, GetConnection());
                result = objCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }

            finally
            {
                CloseConnection();
            }
            return result;

        }

        public SQLiteDataReader ExecuteReader(string query)
        {
            SQLiteDataReader objDR;

            try
            {
                SQLiteCommand objCmd = new SQLiteCommand(query, GetConnection());
                objDR = objCmd.ExecuteReader();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return objDR;

        }

        private void CreateTable()
        {
            // string query = "create table TB_Login(ID Integer primary key AUTOINCREMENT,USERNAME VARCHAR(20),PASSWORD VARCHAR(20));";

            //  string query = "create table TB_Items(ID Integer primary key AUTOINCREMENT,ITEMTYPE VARCHAR(30));";

            //string query = "create table TB_Seller(ID Integer primary key AUTOINCREMENT,SELLER VARCHAR(30),ADDRESS varchar(150));";
            //string query = "drop table TB_ItemInStcok";
            //    string query = "create table TB_ItemInStcok(ID Integer primary key AUTOINCREMENT,ITEMID int,PURCHASERNAME VARCHAR(30),ITEMTYPE varchar(30),ITEM VARCHAR(50),SERIALNUMBER VARCHAR(50),UNIQUENUMBER VARCHAR(20),PRICE varchar(15),PDATE integer,ISDELETED int);";


            // string query = "create table TB_SoldItems(ID Integer primary key AUTOINCREMENT,INVOICENUM int,INVOCENUMWITHYEAR varchar(30),BUYER VARCHAR(30),ADDRESS VARCHAR(150),SERIALNUMBER VARCHAR(50),UNIQUENUMBER VARCHAR(20),PRICE varchar(15),SDATE VARCHAR(20),VAT VARCHAR(20));";
            //  string query = "insert into TB_SoldItems (INVOICENUM,INVOCENUMWITHYEAR,BUYER,ADDRESS,SERIALNUMBER,UNIQUENUMBER,PRICE,SDATE,VAT) values (1001, '1001-07-08' , 'ms sol' ,'hub','cr011','pts2007011','2000','1-1-2007','14');";

         // string query = "delete from TB_ItemInStcok";
        // string query = "delete from TB_SoldItems";
       // string query = "delete from TB_Seller";
      //string query = "delete from TB_Items";
      //   int result = ExecuteNonQuery(query);
        }

    }
}
