using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model_Layer.Models
{
    public class EmailConfirmation
    {
        public Guid ConfirmationID { get; set; }
        public int ClientID { get; set; }
        public bool LinkUsed { get; set; }
        public DateTime ConfirmationDateTime { get; set; }
        public DateTime ExpiryDateTime { get; set; }
    }
}