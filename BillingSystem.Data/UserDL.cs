using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//private namespaces
using BillingSystem.Entities;
using BillingSystem.Utility;

namespace BillingSystem.Data
{
   public class UserDL
   {
       #region Private variables
       bool isSuccess = false;
       int result=0;
       SQLiteHelper objSQLiteHelper=null;
       #endregion
       public bool Login(UserEntity entity)
       {

           
           objSQLiteHelper = new SQLiteHelper();
           int result = 0;

           try
           {
               string query = "select count(*) from TB_Login where USERNAME='" + entity.UserName + "' and PASSWORD='" + entity.Password + "'"; 

               result = Convert.ToInt32(objSQLiteHelper.ExecuteScalar(query));
               if (result > 0)
               {
                   isSuccess = true;
               }
               else
               {
                   isSuccess = false;
               }
           }
           catch (Exception ex)
           {

               throw;
           }
           
           return isSuccess;
       }
       public bool CheckUserName(string username)
       {
           objSQLiteHelper = new SQLiteHelper();
           int result = 0;

           try
           {
               string query = "select count(*) from TB_Login where USERNAME='" + username+"'";

               result =Convert.ToInt32(objSQLiteHelper.ExecuteScalar(query));
               if (result > 0)
               {
                   isSuccess = true;
               }
               else
               {
                   isSuccess = false;
               }
           }
           catch (Exception ex)
           {
              
               throw;
           }
           

           return isSuccess;
       }
       public int Register(UserEntity entity)
       {
          objSQLiteHelper = new SQLiteHelper();
           int result = 0;

           try
           {
               string query = "insert into TB_Login(USERNAME,PASSWORD) values('" + entity.UserName + "','" + entity.Password + "')";

               result = objSQLiteHelper.ExecuteNonQuery(query);
               
              
           }
           catch (Exception ex)
           {


               throw;
           }
           return result;
       }
    }
}
