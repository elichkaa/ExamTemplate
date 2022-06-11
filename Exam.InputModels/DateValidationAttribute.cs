using System.ComponentModel.DataAnnotations;

namespace Exam.InputModels
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Convert.ToDateTime(value) >= DateTime.Now;
        }
    }
}
