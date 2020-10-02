using System;
using Model_Layer.Models;

namespace Model_Layer.Models
{
    public class Client : Common
    {
        public int ClientID { get; set; }
        public int TeamID { get; set; }
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
        public int TeamClientID { get; set; }
    }

   
}