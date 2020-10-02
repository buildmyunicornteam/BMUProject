using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ALMS_DAL;
using Model_Layer.Models;

namespace BuildMyUnicorn.Business_Layer
{
    public class Master
    {
        public IEnumerable<MasterCommon> GetMasterByTableName(string TableName)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@TableName", ParamterValue = TableName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }
            };
            return obj.GetList<MasterCommon>(CommandType.StoredProcedure, "sp_get_master_by_tablename", parameters);

        }
    }
}