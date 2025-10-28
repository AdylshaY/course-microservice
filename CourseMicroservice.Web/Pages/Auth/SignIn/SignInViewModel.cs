using System.ComponentModel.DataAnnotations;

namespace CourseMicroservice.Web.Pages.Auth.SignIn
{
    public record SignInViewModel
    {
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public required string Email { get; init; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; init; }

        public static SignInViewModel Empty => new()
        {
            Email = string.Empty,
            Password = string.Empty,
        };

        /// <summary>
        /// For testing purpose.
        /// </summary>
        public static SignInViewModel GetExampleModel => new()
        {
            Email = "adylsha.yumayev@example.com",
            Password = "Password123",
        };
    }
}
