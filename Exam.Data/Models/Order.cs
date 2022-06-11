using System.ComponentModel.DataAnnotations;

namespace Exam.Data.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Address { get; set; }

        public string? Image { get; set; }

        public string Status { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? CheckedByTechnicianOn { get; set; }

        public string? SupervisorTechnicianName { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
