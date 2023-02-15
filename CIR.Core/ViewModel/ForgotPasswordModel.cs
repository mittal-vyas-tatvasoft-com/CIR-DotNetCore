using System.ComponentModel.DataAnnotations;

namespace CIR.Core.ViewModel
{
    public class ForgotPasswordModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
