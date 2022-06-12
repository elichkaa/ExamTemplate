namespace Exam.InputModels
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class AddRoomInputModel
    {

        [Required]
        [Display(Name = "Room name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Room description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Room location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Room price")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Room image")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
