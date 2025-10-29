using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseMicroservice.Web.Pages.Auth
{
    public class SignOutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["ToastMessage"] = "You have been signed out successfully.";
            TempData["ToastType"] = "success";

            return RedirectToPage("/Index");
        }
    }
}
