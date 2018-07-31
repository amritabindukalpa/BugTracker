using DefectTrackerModels.Interface;
using System.ComponentModel.DataAnnotations;

namespace DefectTracker.Models
{
    public class Users : IModelBase
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is User Admin")]
        public bool IsAdmin { get; set; }
    }
}