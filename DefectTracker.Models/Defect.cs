using DefectTrackerModels.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace DefectTracker.Models
{
    public class Defect : IModelBase
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Defect Title")]
        public string DefectTitle { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string DefectDescription { get; set; }

        [Required]
        [Display(Name = "Logged on")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Updated on")]
        public Nullable<DateTime> ModifiedDate { get; set; }

        [Display(Name = "Resolved on")]
        public Nullable<DateTime> ResolutionDate { get; set; }
        [Display(Name = "Assigned To")]
        public int UserAssignedId { get; set; }
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Assignee")]
        public Users UserAssigned { get; set; }

        [Display(Name = "Defect Status")]
        public DefectStatus Status { get; set; }

        //public virtual List<Users> UserList { get; set; }

        //public virtual List<DefectStatus> DefectStatuses { get; set; }
    }
}