using Business_Model.Helper;
using System;

namespace Business_Model.Model
{
    public interface IBaseObject
    {
        Guid CreatedBy { get; set; }

        DateTime CreatedDateTime { get; set; }

        bool IsActive { get; set; }

        bool IsDeleted { get; set; }

        Guid? ModifiedBy { get; set; }

        DateTime? ModifiedDateTime { get; set; }

        EntityState EntityState { get; set; }
    }
}