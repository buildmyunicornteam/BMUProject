using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model_Layer.Models
{
    public class Common
    {
        public string CreatedName { get; set; }
        public string ModifiedName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
      

    }
}