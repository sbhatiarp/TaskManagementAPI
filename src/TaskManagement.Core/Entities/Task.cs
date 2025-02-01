using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Core.Entities
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public bool IsCompleted { get; set; }
    }
} 