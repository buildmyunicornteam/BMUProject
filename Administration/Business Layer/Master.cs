using ALMS_DAL;
using Model_Layer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Administration.Business_Layer
{
    public class Master
    {

        public string AddMaster(int TableType, MasterCommon Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@TableType", ParamterValue = TableType, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Value", ParamterValue = Model.Value, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@DisplayOrder", ParamterValue = Model.DisplayOrder, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue = Convert.ToInt32(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_master", parameters);
            return result > 0 ? "OK" : "Value already exists";


        }

        public string UpdateMaster(int TableType, MasterCommon Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@TableType", ParamterValue = TableType, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ID", ParamterValue = Model.ID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Value", ParamterValue = Model.Value, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@DisplayOrder", ParamterValue = Model.DisplayOrder, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ModifiedBy", ParamterValue = Convert.ToInt32(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_master", parameters);
            return result > 0 ? "OK" : "Value already exists";


        }

        public IEnumerable<MasterCommon> GetMasterByTableName(string TableName)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@TableName", ParamterValue = TableName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<MasterCommon>(CommandType.StoredProcedure, "sp_get_master_by_tablename", parameters);

        }
        public MasterCommon GetMasterByTableNameAndID(string TableName, Guid ID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@TableName", ParamterValue = TableName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ID", ParamterValue = ID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetSingle<MasterCommon>(CommandType.StoredProcedure, "sp_get_master_by_id", parameters);

        }
        
    }
}