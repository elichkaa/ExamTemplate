namespace Exam.ViewModels
{
    public class OrderOnAdminPageViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }

        public string StatusName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? CheckedByTechnicianOn { get; set; }

        public string? SupervisorTechnicianName { get; set; }

        public string Username { get; set; }
    }
}
