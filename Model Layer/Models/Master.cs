using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model_Layer.Models
{

    public class MasterCommon : Common
    {
        public Guid ID { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class Startup : MasterCommon
    {
        public string TableName { get; } = "tbl_master_startup";
    }
    public class Technology : MasterCommon
    {
        public string TableName { get; } = "tbl_master_technology";
    }
    public class Selling : MasterCommon
    {
        public string TableName { get; } = "tbl_master_selling";
    }
    public class Charge : MasterCommon
    {
        public string TableName { get; } = "tbl_master_charge";
    }
    public class MoneyRasie : MasterCommon
    {
        public string TableName { get; } = "tbl_master_moneyraise  ";
    }
  
}