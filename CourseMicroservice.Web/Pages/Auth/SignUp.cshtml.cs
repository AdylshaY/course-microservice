using CourseMicroservice.Web.Pages.Auth.SignUp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseMicroservice.Web.Pages.Auth
{
    public class SignUpModel(SignUpService signUpService) : PageModel
    {
        [BindProperty]
        public SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.GetExampleModel;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            var result = await signUpService.CreateAccount(SignUpViewModel);
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

            TempData["ToastMessage"] = "The user was successfully created. Please log in.";
            TempData["ToastType"] = "success";

            return RedirectToPage("/index");
        }
    }
}
