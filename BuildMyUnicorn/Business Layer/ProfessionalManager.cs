using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using ALMS_DAL;
using Model_Layer.Models;
using Model_Layer.Helper;
namespace BuildMyUnicorn.Business_Layer
{
    public class ProfessionalManager
    {
        public void AddClientQuestionData(List<_QuestionData> ModelList, Guid SupplierID, Guid SQFID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            
           
            ModelList.ForEach(x => x.SupplierID = SupplierID);
            ModelList.ForEach(x => x.ClientID = Guid.Parse(HttpContext.Current.User.Identity.Name));
            ModelList.ForEach(x => x.SQFID = SQFID);
            ModelList.ForEach(x => x.CreatedDateTime = DateTime.Now);
            DataTable dtSurveyData = Extensions.ListToDataTable(ModelList);
            obj.ExecuteBulkInsert("sp_add_client_question_data", dtSurveyData, "UT_Question_Data", "@DataTable");
        }

        public string AddSupplierClient(_supplierclinet Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = Model.SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SupplierPackageID", ParamterValue = Model.SupplierPackageID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
               

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_supplier_client", parameters);
            return result > 0 ? "OK" : "Error in Adding Package";


        }


        public IEnumerable<Supplier> GetSupplierList()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<Supplier>(CommandType.StoredProcedure, "sp_get_all_supplier", null);
        }

        public IEnumerable<Package> GetSupplierPackageList(Guid SupplierID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<Package>(CommandType.StoredProcedure, "sp_get_supplier_package", parameters);
        }

        public Supplier GetSingleSupplier(Guid SupplierID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<Supplier>(CommandType.StoredProcedure, "sp_get_supplier_by_id", parameters);
        }
        public SupplierQuestion GetSupplierQuestionForm(Guid SupplierID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SupplierID", ParamterValue = SupplierID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<SupplierQuestion>(CommandType.StoredProcedure, "sp_get_supplier_question_form", parameters);
        }
        

    }
}