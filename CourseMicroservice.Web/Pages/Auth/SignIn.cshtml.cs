using CourseMicroservice.Web.Pages.Auth.SignIn;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseMicroservice.Web.Pages.Auth
{
    public class SignInModel(SignInService signInService) : PageModel
    {
        [BindProperty]
        public required SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExampleModel;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            var result = await signInService.AuthenticateAsync(SignInViewModel);

            if (result.IsFail)
            {
                string errorMessage = result.Fail!.Title!;
                if (!string.IsNullOrEmpty(result.Fail.Detail))
                {
                    errorMessage += $": {result.Fail.Detail}";
                }

                TempData["ToastMessage"] = errorMessage;
                TempData["ToastType"] = "error";

                return RedirectToPage();
            }

            TempData["ToastMessage"] = "Signed in successfully.";
            TempData["ToastType"] = "success";

            return RedirectToPage("/index");
        }
    }
}
