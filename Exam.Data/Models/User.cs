using Microsoft.AspNetCore.Identity;

namespace Exam.Data.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<Request> Requests { get; set; }
    }
}
