using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using PinkScoutClasslib;

namespace PinkScoutApp.Pages;

public class IndexModel : PageModel
{
    private readonly LoginRepository _loginRepository;

    public IndexModel(LoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    [BindProperty]
    public CredentialsInput Input { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Page();

        var user = await _loginRepository.ValidateCredentialsAsync(
                Input.Username,
                Input.Password,
                cancellationToken);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            CreatePrincipal(user));

        if (user.IsLeader)
            return RedirectToPage("/LeaderPage");
        else
            return RedirectToPage("/ScoutPage");
    }

    private static ClaimsPrincipal CreatePrincipal(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Name)
        };

        if (user.IsLeader)
            claims.Add(new Claim(ClaimTypes.Role, "leader"));
        else
            claims.Add(new Claim(ClaimTypes.Role, "scout"));

        return new ClaimsPrincipal(new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme));
    }

    public sealed class CredentialsInput
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
    }
}
