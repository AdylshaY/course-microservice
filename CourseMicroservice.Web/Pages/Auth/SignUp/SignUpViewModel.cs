namespace CourseMicroservice.Web.Pages.Auth.SignUp
{
    using System.ComponentModel.DataAnnotations;

    public record SignUpViewModel(
        [Display(Name = "First Name: ")]
        [Required(ErrorMessage = "First Name is required")]
        string FirstName,

        [Display(Name = "Last Name: ")]
        [Required(ErrorMessage = "Last Name is required")]
        string LastName,

        [Display(Name = "Username: ")]
        [Required(ErrorMessage = "Username is required")]
        string Username,

        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        string Email,

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Password is required")]
        string Password,

        [Display(Name = "Confirm Password: ")]
        [Required(ErrorMessage = "Password Confirm is required")]
        string PasswordConfirm
    )
    {
        public static SignUpViewModel Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        /// <summary>
        /// For testing purpose.
        /// </summary>
        public static SignUpViewModel GetExampleModel => new("Adylsha", "Yumayev", "adylsha.yumayev", "adylsha.yumayev@example.com", "Password123", "Password123");
    }
}
