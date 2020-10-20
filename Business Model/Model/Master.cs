using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business_Model.Model
{
    public enum OptionType
    {
       
        WorkLocation = 1,
        BusinessPlacement = 2,
        Charge = 3,
        MoneyRaise = 4,
        Selling = 5,
        Startup = 6,
        Technology = 7
    }

    public class MasterCommon : Common
    {
        public Guid ID { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class Option : Common
    {
        public Guid ID { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
        public OptionType Type { get; set; }
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