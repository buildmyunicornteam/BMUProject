using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Helper
{
    public sealed class ResponseModel
    {
        public Guid EntityID { get; set; }

        public bool HasError { get; set; }

        public int RecordsAffected { get; set; }

        public string Error { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
