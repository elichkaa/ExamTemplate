namespace Exam.InputModels
{
    public class EditOrderByAdminInputModel
    {
        public int Id { get; set; }
        public string? SupervisorTechnicianName { get; set; }

        [DateValidation(ErrorMessage = "Date is in the past")]
        public DateTime? CheckedByTechnicianOn { get; set; }
    }
}
