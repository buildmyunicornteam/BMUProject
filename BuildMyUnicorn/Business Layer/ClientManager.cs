using ALMS_DAL;
using Model_Layer.Helper;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace BuildMyUnicorn.Business_Layer
{
    public class ClientManager
    {
        public string AddNewClient(Client Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@FirstName", ParamterValue = Model.FirstName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LastName", ParamterValue = Model.LastName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Keygen.Random()), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Phone", ParamterValue = Model.Phone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client", parameters);
            if (result > 0)
            {

                List<ParametersCollection> Customerparameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
                Client Customer = obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_email", Customerparameters);
                Guid Ref_id = Guid.NewGuid();
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, VirtualPathUtility.ToAbsolute("~/"));
                string ForgotPasswordURL = strUrl;
                string EncryptedID = Encryption.EncryptGuid(Ref_id.ToString());
                ForgotPasswordURL = ForgotPasswordURL + "/Register/EmailVerification?refid=" + EncryptedID;
                string ForgotEmailTemplate =  EmailTemplates.Template["FP"];
                ForgotEmailTemplate = ForgotEmailTemplate.Replace("@URL", ForgotPasswordURL).Replace("@NAME", Customer.FirstName + " " + Customer.LastName);
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
                new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Customer.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },

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

        public string AddClientTeam(ClientTeam Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RoleInCompany", ParamterValue = Model.RoleInCompany, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FirstName", ParamterValue = Model.FirstName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LastName", ParamterValue = Model.LastName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Phone", ParamterValue = Model.Phone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ImageID", ParamterValue = Model.ImageID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LinkedProfile", ParamterValue = Model.LinkedProfile, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ShortBio", ParamterValue = Model.ShortBio, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
                
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_team", parameters);
            return result > 0 ? "OK" : "Error in Adding Team"; 
          
           
        }

        public string AddClientSurvey(Survey Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SurveyTitle", ParamterValue = Model.SurveyTitle, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SurveyForm", ParamterValue = Model.SurveyForm, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_survey", parameters);
            return result > 0 ? "OK" : "Error in Adding Survey";


        }

        public void AddSurveyData(List<SurveyData> ModelList, Guid SurveyID)
        {
             DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            //List<ParametersCollection> parameters = new List<ParametersCollection>() {
            //    new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Convert.ToInt16(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
            //    new ParametersCollection { ParamterName = "@SurveyTitle", ParamterValue = Model.SurveyTitle, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
            //    new ParametersCollection { ParamterName = "@SurveyForm", ParamterValue = Model.SurveyForm, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
            //    new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue = Convert.ToInt16(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            //};
            ModelList.ForEach(x => x.SurveyDataID = new Guid());
            ModelList.ForEach(x => x.SurveyID = SurveyID);
            ModelList.ForEach(x => x.CreatedDateTime = DateTime.Now);
            DataTable dtSurveyData = Extensions.ListToDataTable(ModelList);
            obj.ExecuteBulkInsert("sp_add_survey_data", dtSurveyData, "UT_Survey_Data", "@DataTable");
        }


        public Client GetClient(Guid ClientID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_id", parameters);
        }

        public IEnumerable<ClientTeam> GetClientTeam()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<ClientTeam>(CommandType.StoredProcedure, "sp_get_client_team", parameters);
        }

        public IEnumerable<Survey> GetClientAllSurveyForm()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<Survey>(CommandType.StoredProcedure, "sp_get_client_all_survey", parameters);
        }

        public Survey GetClientSurveyForm(Guid SurveyID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SurveyID", ParamterValue = SurveyID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<Survey>(CommandType.StoredProcedure, "sp_get_client_survey", parameters);
        }

        public IEnumerable<SurveyData>  GetSurveyData(Guid SurveyID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SurveyID", ParamterValue = SurveyID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<SurveyData>(CommandType.StoredProcedure, "sp_get_survey_data", parameters);
        }
        public string UpdateClientProfile(Client Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RoleInCompany", ParamterValue = Model.RoleInCompany, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ImageID", ParamterValue = Model.ImageID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LinkedProfile", ParamterValue = Model.LinkedProfile, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ShortBio", ParamterValue = Model.ShortBio, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_client_profile", parameters);
            return result > 0 ? "OK" : "Error in Adding Team";


        }

        public string UpdateClientTeam(ClientTeam Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RoleInCompany", ParamterValue = Model.RoleInCompany, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FirstName", ParamterValue = Model.FirstName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LastName", ParamterValue = Model.LastName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Phone", ParamterValue = Model.Phone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ImageID", ParamterValue = Model.ImageID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LinkedProfile", ParamterValue = Model.LinkedProfile, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ShortBio", ParamterValue = Model.ShortBio, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ModifiedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_client_team", parameters);
            return result > 0 ? "OK" : "Error in Adding Team";


        }


        public string UpdateCustomerPassword(Client Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Model.Password), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = 1, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_password", parameters);
            if (result > 0) return "OK"; else return "Password update Failed, Please Try again";

        }

        public void UpdateCustomerEmailConfirmation(Client Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ID", ParamterValue = Model.ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            obj.Execute(CommandType.StoredProcedure, "sp_update_email_confirmation", parameters);

        }

        public void UpdateCustomerCustomerForgotPassword(Client Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@id", ParamterValue = Model.ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            obj.Execute(CommandType.StoredProcedure, "sp_update_forgot_password", parameters);

        }

        public string UpdateSurveyStatus(Guid SurveyID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SurveyID", ParamterValue = SurveyID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_survey_active_status", parameters);
            return result > 0 ? "OK" : "Error in Survey Active/Inactive";
        }

        public string ValidateCustomerLogin(Client Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
            Client Customer = obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_email", parameters);
            if (Customer != null)
            {


                if (!Customer.IsDeleted)
                {
                    if (Customer.IsActive)
                    {

                        if (Encryption.Encrypt(Model.Password) == Customer.Password)
                        {
                            FormsAuthentication.SetAuthCookie(Customer.ClientID.ToString(), true); return "OK";
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
            else
            {
                return "Invalid Username or Password";
            }
        }


        public string[] ConfirmEmail(string refid)
        {
            string[] returnvalue = new string[4];
            try
            {

                Guid ConfirmationID = Guid.Parse(Encryption.DecryptGuid(refid));
                DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
                List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@id", ParamterValue = ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
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
                           // returnvalue[3] = new ClientManager().GetClient(link.EntityID).FirstName.ToString();
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

        public string DeleteSurvey(Guid SurveyID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SurveyID", ParamterValue = SurveyID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            int result =  obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_client_survey", parameters);
            return result > 0 ? "OK" : "Error in Delete";
        }


    }
}