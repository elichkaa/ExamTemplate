namespace Exam.ViewModels
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinalDate { get; set; }

        public string RoomName { get; set; }

        public string Username { get; set; }

        public decimal FinalPrice { get; set; }

        public string Status { get; set; }
    }
}
