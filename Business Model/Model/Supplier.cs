using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business_Model.Model
{
    public class Supplier : Common
    {
        public Guid SupplierID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string CompanyProfile { get; set; }
        public string Website { get; set; }
        public string Twitter { get; set; }
        public string Address { get; set; }
        public string LinkedProfile { get; set; }
        public string ImageID { get; set; }
        public string WorkLocation { get; set; }
        public string BusinessPlacement { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionForm { get; set; }
        public string OrginalValue { get; set; }
        public string Password { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public bool IsProfileUpdated { get; set; }
        public Guid ConfirmationID { get; set; }
    }

  

    public class QuestionData
    {
        public Guid QuestionDataID { get; set; }
        public Guid SupplierID { get; set; }
        public Guid ClientID { get; set; }
        public Guid SQFID { get; set; }
        public int QuestionUnitID { get; set; }
        public string KeyField { get; set; }
        public string KeyValue { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }

    public class _QuestionData
    {
     
        public Guid SupplierID { get; set; }
        public Guid ClientID { get; set; }
        public Guid SQFID { get; set; }
        public string KeyField { get; set; }
        public string KeyValue { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }

    public class SupplierQuestion
    {
        public Guid SQFID { get; set; }
        public Guid SupplierID { get; set; }
        public string QuestionForm { get; set; }
        public string QuestionTitle { get; set; }
       
    }

    public class Package :Common
    {
        public Guid SupplierPackageID { get; set; }
        public Guid SupplierID { get; set; }
        public Guid Duration { get; set; }
        public string  PackageTitle { get; set; }
        public decimal PackageAmount { get; set; }
        public string PackageAttribute { get; set; }
        public Guid CurrencyID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }

    }

    public class supplierClient : Common
    {
        public Guid ClientID { get; set; }
        public Guid TeamClientID { get; set; }
        public string PackageTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string StartupName { get; set; }
        public string Phone { get; set; }
        public decimal PackageAmount { get; set; }
    }

    public class _supplierclinet
    {
        public Guid SupplierID { get; set; }
        public Guid ClientID { get; set; }
        public Guid SupplierPackageID { get; set; }
       
    }
}