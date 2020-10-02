using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model_Layer.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public Int64 CountryPhoneCode { get; set; }
        public string FlagURL { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}