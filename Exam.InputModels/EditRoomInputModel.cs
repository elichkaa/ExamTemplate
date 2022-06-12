using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Exam.InputModels
{
    public class EditRoomInputModel
    {
        public int Id { get; set; }

        [Display(Name = "Room name")]
        public string Name { get; set; }

        [Display(Name = "Room description")]
        public string Description { get; set; }

        [Display(Name = "Room location")]
        public string Location { get; set; }

        [Display(Name = "Room price")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public decimal Price { get; set; }

        [Display(Name = "Room image")]
        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }
    }
}
