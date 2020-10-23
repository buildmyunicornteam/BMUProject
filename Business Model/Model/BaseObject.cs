using Business_Model.Helper;
using System;

namespace Business_Model.Model
{
    public class BaseObject : IBaseObject
    {
        [IgnoreUpdate]
        public Guid CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        [IgnoreUpdate]
        public DateTime CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; }

        [IgnoreInsert]
        public EntityState EntityState { get; set; }
    }
}
