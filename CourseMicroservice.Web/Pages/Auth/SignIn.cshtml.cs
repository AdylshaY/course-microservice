using CourseMicroservice.Web.Pages.Auth.SignIn;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseMicroservice.Web.Pages.Auth
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public required SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExampleModel;
        public void OnGet()
        {
        }
    }
}
