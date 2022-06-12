namespace Exam.InputModels
{
    public class EditRequestInputModel
    {
        public int Id { get; set; }

        [DateValidation]
        public DateTime StartDate { get; set; }

        [DateValidation]
        public DateTime FinalDate { get; set; }

        public string RoomName { get; set; }

        public string Username { get; set; }

        public decimal FinalPrice { get; set; }

        public string Status { get; set; }
    }
}
