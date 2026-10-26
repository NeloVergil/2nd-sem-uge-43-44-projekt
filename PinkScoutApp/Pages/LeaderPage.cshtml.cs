using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PinkScoutApp.Pages;

[Authorize(Roles = "leader")]
public class LeaderPageModel : PageModel
{
    public void OnGet()
    {
    }
}
