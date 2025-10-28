namespace CourseMicroservice.Web.Pages.Auth.SignUp
{
    using System.ComponentModel.DataAnnotations;

    public record SignUpViewModel
    {
        [Display(Name = "First Name: ")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; init; }

        [Display(Name = "Last Name: ")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; init; }

        [Display(Name = "Username: ")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; init; }

        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; init; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }

        [Display(Name = "Confirm Password: ")]
        [Required(ErrorMessage = "Password Confirm is required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; init; }

        public static SignUpViewModel Empty => new()
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            Username = string.Empty,
            Email = string.Empty,
            Password = string.Empty,
            PasswordConfirm = string.Empty
        };

        /// <summary>
        /// For testing purpose.
        /// </summary>
        public static SignUpViewModel GetExampleModel => new()
        {
            FirstName = "Adylsha",
            LastName = "Yumayev",
            Username = "adylsha.yumayev",
            Email = "adylsha.yumayev@example.com",
            Password = "Password123",
            PasswordConfirm = "Password123"
        };
    }
}
