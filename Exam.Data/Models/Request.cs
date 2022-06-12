using System.ComponentModel.DataAnnotations;

namespace Exam.Data.Models
{
    public class Request
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinalDate { get; set; }

        public int? RoomId { get; set; }

        public Room? Room { get; set; }  

        public string? UserId { get; set; }

        public User? User { get; set; }

        public decimal FinalPrice { get; set; }

        [Required]
        public string? Status { get; set; }
    }
}
