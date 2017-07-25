using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//custom namespaces
using BillingSystem.Entities;
using BillingSystem.Data;

namespace BillingSystem.Business
{
    public class UserBL
    {
        #region Private variables
        UserEntity objuserEntity=new UserEntity();
        UserDL objUserDL = new UserDL();
        bool result = false;
        int success = 0;
        #endregion

        public bool Login(UserEntity entity)
        {
            try
            {
                result = objUserDL.Login(entity);
            }
            catch (Exception ex)
            {

                throw;
            }   
        
         return result;
        }

        public bool checkUserName(string username)
        {
            try
            {
               result= objUserDL.CheckUserName(username);
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        public int Register(UserEntity entity)
        {
            try
            {
                success = objUserDL.Register(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
            return success; 

        }
    }
}
