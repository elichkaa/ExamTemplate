namespace Exam.InputModels
{
    public class EditUserInputModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<string> Roles { get; set; }

        public ICollection<string> AllRoles { get; set; }
    }
}
