using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    [NotMapped] // don't add table to db
    public class LoginUser
    {
        [Display(Name = "Email Address:")]
        [Required(ErrorMessage = "is required.")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string EmailLogin { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(8, ErrorMessage = "must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string PasswordLogin { get; set; }
    }
}