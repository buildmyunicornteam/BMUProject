using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business_Model.Model
{
    public class Common
    {
        public string CreatedName { get; set; }
        public string ModifiedName { get; set; }
       // public int CreatedBy { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
      

    }
}