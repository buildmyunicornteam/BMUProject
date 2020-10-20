using ALMS_DAL;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Administration.Business_Layer
{
    public class AdminManager
    {
        public string ValidateAdminLogin(AdminUser Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
            AdminUser User = obj.GetSingle<AdminUser>(CommandType.StoredProcedure, "sp_get_admin_user_by_email", parameters);
            if (User != null)
            {
                if (User.IsActive)
                    {

                        if (Model.Password == User.Password)
                        {
                            FormsAuthentication.SetAuthCookie(User.AdminUserID.ToString(), true);
                            return "OK";
                        }
                        else
                        {
                            return "Invalid Username or Password";
                        }
                    }
                    else
                    {
                        return "Your account is in-active";


                    }
                }
                else
                {
                    return "Invalid Username or Password";
                }
            
           
        }
    }
}