namespace CourseMicroservice.Web.Pages.Auth.SignUp
{
    using System.ComponentModel.DataAnnotations;

    public record SignUpViewModel(
        [Display(Name = "First Name: ")] string FirstName,
        [Display(Name = "Last Name: ")] string LastName,
        [Display(Name = "Username: ")] string Username,
        [Display(Name = "Email: ")] string Email,
        [Display(Name = "Password: ")] string Password,
        [Display(Name = "Confirm Password: ")] string PasswordConfirm
    )
    {
        public static SignUpViewModel Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        /// <summary>
        /// For testing purpose.
        /// </summary>
        public static SignUpViewModel GetExampleModel => new("Adylsha", "Yumayev", "adylsha.yumayev", "adylsha.yumayev@example.com", "Password123", "Password123");
    }
}
