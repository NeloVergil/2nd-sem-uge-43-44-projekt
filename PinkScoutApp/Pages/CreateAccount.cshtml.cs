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
            cancellationToken);

        if (!created)
        {
            ModelState.AddModelError(
                nameof(Input.Username),
                "That username is already in use.");
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