using System.ComponentModel.DataAnnotations;

namespace Exam.InputModels
{
    public class AddRequestClientInputModel
    {
        [Required]
        [DateValidation]
        public DateTime StartDate { get; set; }

        [Required]
        [DateValidation]
        public DateTime FinalDate { get; set; }

        [Required]
        public string RoomName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public decimal FinalPrice { get; set; }
    }
}
