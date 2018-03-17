namespace Lands.Backend.Models
{
    using Lands2.Domain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped] //Para que, como User tira a la base de datos, UserView no lo haga
    public class UserView : User
    {
        [Display(Name = "Picture")]
        public HttpPostedFileBase PictureFile { get; set; }

        [DataType(DataType.Password)]        
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, ErrorMessage = "The length for field {0} must be between {1} and {2} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field {0} is required.")]
        [Compare("Password", ErrorMessage = "The password and its confirmation does not mach")]
        [Display(Name = "Password confirm")]
        public string PasswordConfirm { get; set; }        
    }
}