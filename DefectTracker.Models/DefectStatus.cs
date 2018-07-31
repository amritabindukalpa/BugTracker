using DefectTrackerModels.Interface;
using System.ComponentModel.DataAnnotations;

namespace DefectTracker.Models
{
    public class DefectStatus : IModelBase
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}