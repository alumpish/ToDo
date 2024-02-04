using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ToDo.Models
{
    public class ToDoTask
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }
        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}
