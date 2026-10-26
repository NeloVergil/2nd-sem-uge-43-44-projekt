using Microsoft.AspNetCore.Mvc.RazorPages;
using PinkScoutClasslib;
namespace PinkScoutApp.Pages;

public class ActivityDetails : PageModel
{
    public Activity at { get; set; }

    private ActivityService _activityService;
    
    public ActivityDetails(ActivityService ActivityService)
    {
        _activityService = ActivityService;
    }
    
    
    public void OnGet(int id)
    {
        at = _activityService.GetActivity(id);
    }
}