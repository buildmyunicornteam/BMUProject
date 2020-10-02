using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model_Layer.Models
{
    public class AdminUser :Common
    {
        public int AdminUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime CurrentLogin { get; set; }
        public DateTime LastLogin { get; set; }
    }
}