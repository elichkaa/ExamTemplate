using System.ComponentModel.DataAnnotations;

namespace Exam.Data.Models
{
    public class Room
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageId { get; set; }

        public Image Image { get; set; }
    }
}
