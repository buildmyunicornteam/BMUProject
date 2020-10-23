using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Helper
{

    public enum ToDoStatus
    {
        Active = 0,

        InActive = 1,

        Completed = 2
    }


    public enum EntityState
    {
        New = 0,

        Modified = 1,

        Old = 2,

        Deleted = 3
    }


}
