﻿using ALMS_DAL;
using Model_Layer.Helper;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace BuildMyUnicorn_Supplier.Business_Layer
{
    public class SupplierManager
    {
        public string AddNewSupplier (Supplier Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@CompanyName", ParamterValue = Model.CompanyName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FirstName", ParamterValue = Model.FirstName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LastName", ParamterValue = Model.LastName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_supplier", parameters);
            if (result > 0)
            {

                List<ParametersCollection> Customerparameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
                Supplier Supplier = obj.GetSingle<Supplier>(CommandType.StoredProcedure, "sp_get_supplier_by_email", Customerparameters);
                Guid Ref_id = Guid.NewGuid();
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, VirtualPathUtility.ToAbsolute("~/"));
                string ForgotPasswordURL = strUrl;
                string EncryptedID = Encryption.EncryptGuid(Ref_id.ToString());
                ForgotPasswordURL = ForgotPasswordURL + "/Register/EmailVerification?refid=" + EncryptedID;
                string ForgotEmailTemplate = EmailTemplates.Template["FP"];
                ForgotEmailTemplate = ForgotEmailTemplate.Replace("@URL", ForgotPasswordURL).Replace("@NAME", Supplier.FirstName + " " + Supplier.LastName);
                string SenderEmail = ConfigurationManager.AppSettings["SmtpServerUsername"];
                //Finally Send Mail and save data Async
                Thread email_sender_thread = new Thread(delegate ()
                {
                    EmailSender emailobj = new EmailSender();
                    emailobj.SendMail(SenderEmail, Model.Email, "Build my Unicorn Account", ForgotEmailTemplate);
                });

                Thread SaveRestLink = new Thread(delegate ()
                {
                List<ParametersCollection> parametersConfirmation = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ID", ParamterValue = Ref_id, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Supplier.SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },

            };
                    obj.Execute(CommandType.StoredProcedure, "sp_add_email_confirmation", parametersConfirmation);


                });
                email_sender_thread.IsBackground = true;
                email_sender_thread.Start();
                SaveRestLink.Start();
                return "OK";
            }
            else
            {
                return "Email is already registered";
            }
        }

        public string AddNewQuestionForm(Supplier Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SQFID", ParamterValue = Guid.NewGuid(), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@QuestionForm", ParamterValue = Model.QuestionForm, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@QuestionTitle", ParamterValue = Model.QuestionTitle, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_supplier_question_form", parameters);
            return result > 0 ? "OK" : "Question Form Added Failed";  

        }

     

        public Supplier GetSingleSupplier(Guid SupplierID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<Supplier>(CommandType.StoredProcedure, "sp_get_supplier_by_id", parameters);
        }

        public SupplierQuestion GetSupplierQustionForm()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<SupplierQuestion>(CommandType.StoredProcedure, "sp_get_supplier_question_form", parameters);
        }

        public IEnumerable<supplierClient> GetSupplierClientList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<supplierClient>(CommandType.StoredProcedure, "sp_get_supplier_all_client", parameters);
        }

        public IEnumerable<Currency> GetCurrencyList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            return obj.GetList<Currency>(CommandType.StoredProcedure, "sp_get_all_currency", null);
        }

        public Client GetClient(Guid ClientID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_id", parameters);
        }

        public IEnumerable<QuestionData> GetSupplierClientData(Guid ClientID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<QuestionData>(CommandType.StoredProcedure, "sp_get_supplier_question_data_by_client", parameters);
        }

        public string UpdateSupplierProfile(Supplier Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@SupplierID", ParamterValue =Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@CompanyName", ParamterValue = Model.CompanyName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@CompanyProfile", ParamterValue = Model.CompanyProfile, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Website", ParamterValue = Model.Website, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@LinkedProfile", ParamterValue = Model.LinkedProfile, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ImageID", ParamterValue = Model.ImageID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@WorkLocation", ParamterValue = Model.WorkLocation, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@BusinessPlacement", ParamterValue = Model.BusinessPlacement, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Twitter", ParamterValue = Model.Twitter, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Address", ParamterValue = Model.Address, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_supplier", parameters);
            if (result > 0) return "OK"; else return "Company Profile update Failed, Please Try again";

        }

        public string UpdateSupplierPassword(Supplier Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Model.SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Model.Password), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = 2, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_password", parameters);
            if (result > 0) return "OK"; else return "Password update Failed, Please Try again";

        }

        public void UpdateEmailConfirmation(Guid ConfirmationID)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ID", ParamterValue = ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            obj.Execute(CommandType.StoredProcedure, "sp_update_email_confirmation", parameters);

        }

        public string[] ConfirmEmail(string refid)
        {
            string[] returnvalue = new string[4];
            try
            {

                Guid ConfirmationID = Guid.Parse(Encryption.DecryptGuid(refid));
                DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
                List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ID", ParamterValue = ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
                EmailConfirmation link = obj.GetSingle<EmailConfirmation>(CommandType.StoredProcedure, "sp_get_email_confirmation", parameters);
                if (link != null)
                {
                    if (link.LinkUsed == false)
                    {
                        TimeSpan varTime = DateTime.Now - link.ExpiryDateTime;
                        double fractionalMinutes = varTime.TotalMinutes;

                        if (fractionalMinutes <= 107787779999990)
                        {
                            returnvalue[0] = "OK";
                            returnvalue[1] = link.EntityID.ToString();
                            returnvalue[2] = ConfirmationID.ToString();
                            returnvalue[3] = new SupplierManager().GetSingleSupplier(link.EntityID).FirstName.ToString();
                            return returnvalue;
                        }
                        else

                        {
                            returnvalue[0] = "Link Expired";
                            returnvalue[1] = "0";
                            returnvalue[2] = "0";
                            returnvalue[3] = "";
                            return returnvalue;
                        }
                    }

                    else

                    {
                        returnvalue[0] = "Link Is Already Used";
                        returnvalue[1] = "0";
                        returnvalue[2] = "0";
                        returnvalue[3] = "";
                        return returnvalue;
                    }
                }

                else

                {
                    returnvalue[0] = "Invalid Query String";
                    returnvalue[1] = "0";
                    returnvalue[2] = "0";
                    returnvalue[3] = "";
                    return returnvalue;
                }
            }
            catch (Exception)
            {
                returnvalue[0] = "Invalid Query String";
                returnvalue[1] = "0";
                returnvalue[2] = "0";
                returnvalue[3] = "";
                return returnvalue;
            }

        }

        public string[] ConfirmResetPassword(string refid)
        {
            string[] returnvalue = new string[4];
            try
            {

                Guid ConfirmationID = Guid.Parse(Encryption.DecryptGuid(refid));
                DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
                List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@id", ParamterValue = ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
                ForgotPassword link = obj.GetSingle<ForgotPassword>(CommandType.StoredProcedure, "sp_get_reset_password", parameters);
                if (link != null)
                {
                    if (link.LinkUsed == false)
                    {
                        TimeSpan varTime = DateTime.Now - link.ForgotDatetime;
                        double fractionalMinutes = varTime.TotalMinutes;

                        if (fractionalMinutes <= 30)
                        {
                            returnvalue[0] = "OK";
                            returnvalue[1] = link.ClientID.ToString();
                            returnvalue[2] = ConfirmationID.ToString();
                            //  returnvalue[3] = new ClientManager().GetClient().FirstName.ToString();
                            return returnvalue;
                        }
                        else

                        {
                            returnvalue[0] = "Link Expired";
                            returnvalue[1] = "0";
                            returnvalue[2] = "0";
                            returnvalue[3] = "";
                            return returnvalue;
                        }
                    }

                    else

                    {
                        returnvalue[0] = "Link Is Already Used";
                        returnvalue[1] = "0";
                        returnvalue[2] = "0";
                        returnvalue[3] = "";
                        return returnvalue;
                    }
                }

                else

                {
                    returnvalue[0] = "Invalid Query String";
                    returnvalue[1] = "0";
                    returnvalue[2] = "0";
                    returnvalue[3] = "";
                    return returnvalue;
                }
            }
            catch (Exception)
            {
                returnvalue[0] = "Invalid Query String";
                returnvalue[1] = "0";
                returnvalue[2] = "0";
                returnvalue[3] = "";
                return returnvalue;
            }

        }

        public string SendPasswordRestLink(string Email)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> Customerparameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@Email", ParamterValue = Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
            Client Customer = obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_customer_by_email", Customerparameters);
            if (Customer != null)
            {
                Guid Ref_id = Guid.NewGuid();
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, VirtualPathUtility.ToAbsolute("~/"));
                string ForgotPasswordURL = strUrl;
                string EncryptedID = Encryption.EncryptGuid(Ref_id.ToString());
                ForgotPasswordURL = ForgotPasswordURL + "/Signup/ResetPassword?refid=" + EncryptedID;
                string ForgotEmailTemplate = "";// ForgotPasswordTemplate.Template["FP"];
                ForgotEmailTemplate = ForgotEmailTemplate.Replace("@URL", ForgotPasswordURL).Replace("@NAME", Customer.FirstName + " " + Customer.LastName);
                string SenderEmail = ConfigurationManager.AppSettings["SmtpServerUsername"];
                //Finally Send Mail and save data Async
                Thread email_sender_thread = new Thread(delegate ()
                {
                    EmailSender emailobj = new EmailSender();
                    emailobj.SendMail(SenderEmail, Email, "Build my Unicorn Account", ForgotEmailTemplate);
                });

                Thread SaveRestLink = new Thread(delegate ()
                {
                    List<ParametersCollection> parametersConfirmation = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@id", ParamterValue = Ref_id, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Customer.ClientID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },

                };
                    obj.Execute(CommandType.StoredProcedure, "sp_add_password_reset", parametersConfirmation);


                });
                email_sender_thread.IsBackground = true;
                email_sender_thread.Start();
                SaveRestLink.Start();
                return "OK";
            }
            else
            {
                return "Email is not registered";
            }
        }

        public string ValidateLogin(Supplier Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
            Supplier Supplier = obj.GetSingle<Supplier>(CommandType.StoredProcedure, "sp_get_supplier_by_email", parameters);
            if (Supplier != null)
            {
                    if (Supplier.IsActive)
                    {

                        if (Encryption.Encrypt(Model.Password) == Supplier.Password)
                        {
                            FormsAuthentication.SetAuthCookie(Supplier.SupplierID.ToString(), true); return "OK";
                        }
                        else
                        {
                            return "Invalid Username or Password";
                        }
                    }
                    else
                    {
                        return "Your account is not activated, Confirm your email";


                    }
             
               
            }
            else
            {
                return "Invalid Username or Password";
            }
        }


    }
}