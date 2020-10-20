using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business_Model.Model
{
    public class ForgotPassword
    {
        public Guid ForgotPasswordID { get; set; }
        public int ClientID { get; set; }
        public bool LinkUsed { get; set; }
        public DateTime ForgotDatetime { get; set; }
        public DateTime ExpiryDatetime { get; set; }

    }
}