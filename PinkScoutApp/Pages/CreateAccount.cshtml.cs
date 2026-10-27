using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PinkScoutClasslib;

namespace PinkScoutApp.Pages;

public class CreateAccountModel : PageModel
{
    private readonly LoginRepository _loginRepository;

    public CreateAccountModel(LoginRepository loginRepository)
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

        var created = await _loginRepository.CreateUserAsync(
            Input.Username,
            Input.Password,
            Input.IsLeader,
            cancellationToken);

        if (created is null)
        {
            ModelState.AddModelError(
                nameof(Input.Username),
                "That username is already in use.");
            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, created.Name)
        };

        if (created.IsLeader)
            claims.Add(new Claim(ClaimTypes.Role, "leader"));

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme)));
        if (created.IsLeader)
            return RedirectToPage("/LeaderPage");
        else
            return RedirectToPage("/ScoutPage");
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

        public bool IsLeader { get; set; }
    }
}