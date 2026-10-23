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

        if (!await _loginRepository.ValidateCredentialsAsync(
                Input.Username,
                Input.Password,
                cancellationToken))
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }

        return RedirectToPage("/Home");
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
