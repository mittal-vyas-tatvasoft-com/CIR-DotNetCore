using System.ComponentModel.DataAnnotations;

namespace CIR.Core.ViewModel
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Your password must be at least 8 characters long, contain at least one number and have a mixture of uppercase and lowercase letters")]
        public string NewPassword { get; set; }
    }
}
