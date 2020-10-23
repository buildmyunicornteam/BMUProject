using Business_Model.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business_Model.Model
{
    public sealed class ToDoTask : BaseObject
    {
        [PrimaryKey]
        public Guid ToDoTaskID { get; set; }

        [Required]
        public string Subject { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public ToDoStatus Status { get; set; }

        [Range(0, 100)]
        public float? PercentComplete { get; set; }

        public Guid? AssignedTo { get; set; }

        public Guid? AssignedBy { get; set; }

        public DateTime? AssignedOn { get; set; }

        public DateTime? CompletedOn { get; set; }

    }

}
