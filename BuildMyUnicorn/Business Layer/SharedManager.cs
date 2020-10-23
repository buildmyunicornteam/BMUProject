using ALMS_DAL;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace BuildMyUnicorn.Business_Layer
{
    public static class SharedManager
    {
        public static T GetItem<T>(string sqlQuery)
        {
            var connection = GetConnection();
            T obj = connection.GetSingle<T>(CommandType.Text, sqlQuery);

           var property = obj.GetType().GetProperty(nameof(BaseObject.EntityState));
            if (property != null && property.PropertyType.IsEnum)
                property.SetValue(obj, EntityState.Old);

            return obj;
        }

        public static IEnumerable<T> GetList<T>(string sqlQuery)
        {
            var connection = GetConnection();
            return connection.GetList<T>(CommandType.Text, sqlQuery);
        }

        public static int ExecuteProcedure<TModel>(TModel model, string procName)
        {

            var connection = GetConnection();
            List<ParametersCollection> parameters = new List<ParametersCollection>();

            Setparameters(model, parameters);

            return connection.ExecuteWithReturnValue(CommandType.StoredProcedure, procName, parameters);
        }

        public static ResponseModel Save<TModel>(TModel model, string query)
        {
            return Execute(model, query);
        }

        public static ResponseModel Execute<TModel>(TModel model, string query, bool isUpdate = false)
        {
            ResponseModel response = new ResponseModel();
            var connection = GetConnection();
            List<ParametersCollection> parameters = new List<ParametersCollection>();
            StringBuilder queryParameters = Setparameters(model, parameters, isUpdate);

            if (isUpdate)
                query = string.Format(query, queryParameters, isUpdate);
            else
                query = string.Format(query,
                        queryParameters.ToString().Replace("@", string.Empty), queryParameters);

            response.RecordsAffected = connection.ExecuteWithReturnValue(CommandType.Text, query, parameters);
            response.HasError = connection._hasError;
            response.Error = connection._errorMessage;
            return response;
        }

        public static ResponseModel Update<TModel>(TModel model, string query)
        {
            return Execute(model, query, true);
        }

        public static int Delete<TModel>(string query, Guid keyValue)
        {
            var connection = GetConnection();
            var keyProperty = typeof(TModel).GetProperties().FirstOrDefault(x=>x.IsDefined(typeof(PrimaryKeyAttribute), false));

            List<ParametersCollection> parameters = new List<ParametersCollection>
            {
                new ParametersCollection { ParamterName = $"@{keyProperty.Name}", ParamterValue = keyValue, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };

            return connection.ExecuteWithReturnValue(CommandType.Text, query, parameters);
        }


        private static StringBuilder Setparameters<TModel>(TModel model, List<ParametersCollection> parameters, bool isUpdate = false)
        {
            var queryParameters = new StringBuilder();
            var props = model.GetType().GetProperties();
            foreach (var item in props)
            {
                if (item.IsDefined(typeof(IgnoreInsert)) || (item.IsDefined(typeof(IgnoreUpdate)) && isUpdate)) 
                    continue;

                var type = item.PropertyType.IsEnum ? DbType.Int16 : TypeMap[item.PropertyType];
                parameters.Add(new ParametersCollection { ParamterName = $"@{item.Name}", ParamterValue = item.GetValue(model), ParamterType = type, ParameterDirection = ParameterDirection.Input });
                if (isUpdate)
                    queryParameters.Append($", {item.Name} = @{item.Name}");
                else
                    queryParameters.Append($", @{item.Name}");
            };

            if (queryParameters.Length != 0)
                queryParameters = queryParameters.Remove(0, 1);
            return queryParameters;
        }

        public static DataLayer GetConnection()
        {
           return new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        }

        public static void SetBasicProperties(this IBaseObject model)
        {
            if (model.EntityState == EntityState.New)
            {
                model.CreatedDateTime = DateTime.UtcNow;
                model.IsActive = true;
                model.CreatedBy = Guid.Parse(HttpContext.Current.User.Identity.Name);
            }
            else
            {
                model.ModifiedDateTime = DateTime.UtcNow;
                model.ModifiedBy = Guid.Parse(HttpContext.Current.User.Identity.Name);
            }
        }

        #region private methods/fields

        private static readonly Dictionary<Type, DbType> TypeMap = new Dictionary<Type, DbType>
        {
                    {typeof(byte) , DbType.Byte},
                    {typeof(sbyte) , DbType.SByte},
                    {typeof(short) , DbType.Int16},
                    {typeof(ushort) , DbType.UInt16},
                    {typeof(int) , DbType.Int32},
                    {typeof(uint) , DbType.UInt32},
                    {typeof(long) , DbType.Int64},
                    {typeof(ulong) , DbType.UInt64},
                    {typeof(float) , DbType.Single},
                    {typeof(double) , DbType.Double},
                    {typeof(decimal) , DbType.Decimal},
                    {typeof(bool) , DbType.Boolean},
                    {typeof(string) , DbType.String},
                    {typeof(char) , DbType.StringFixedLength},
                    {typeof(Guid) , DbType.Guid},
                    {typeof(DateTime) , DbType.DateTime},
                    {typeof(DateTimeOffset) , DbType.DateTimeOffset},
                    {typeof(byte?) , DbType.Byte},
                    {typeof(sbyte?) , DbType.SByte},
                    {typeof(short?) , DbType.Int16},
                    {typeof(ushort?) , DbType.UInt16},
                    {typeof(int?) , DbType.Int32},
                    {typeof(uint?) , DbType.UInt32},
                    {typeof(long?) , DbType.Int64},
                    {typeof(ulong?) , DbType.UInt64},
                    {typeof(float?) , DbType.Single},
                    {typeof(double?) , DbType.Double},
                    {typeof(decimal?) , DbType.Decimal},
                    {typeof(bool?) , DbType.Boolean},
                    {typeof(char?) , DbType.StringFixedLength},
                    {typeof(Guid?) , DbType.Guid},
                    {typeof(DateTime?) , DbType.DateTime},
                    {typeof(DateTimeOffset?) , DbType.DateTimeOffset}
        };

        #endregion
    }
}