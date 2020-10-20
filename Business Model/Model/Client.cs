using System;
using Business_Model.Model;

namespace Business_Model.Model
{
    public class Client : Common
    {
        public Guid ClientID { get; set; }
        public Guid TeamID { get; set; }
        public string StartupName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string RoleInCompany { get; set; }
        public string LinkedProfile { get; set; }
        public string CountryName { get; set; }
        public string ImageID { get; set; }
        public string ShortBio { get; set; }
        public int CountryID { get; set; }
        public bool IsProfileUpdated { get; set; }
        public Guid ConfirmationID { get; set; }
        public DateTime LastLoginDateTime { get; set; }
    }
    public class ClientTeam : Client
    {
        public Guid TeamClientID { get; set; }
    }

   
}