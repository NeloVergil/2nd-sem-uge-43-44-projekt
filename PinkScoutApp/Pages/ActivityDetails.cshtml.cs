using Microsoft.AspNetCore.Mvc.RazorPages;
using PinkScoutClasslib;
namespace PinkScoutApp.Pages;

public class ActivityDetailsModel : PageModel
{
    public Activity Activity { get; set; }

    private ActivityService _activityService;
    
    
    public ActivityDetailsModel(ActivityService ActivityService)
    {
        _activityService = ActivityService;
    }
    
    public void OnGet(string id)
    {
        Activity = _activityService.GetActivityByName(id);
    }
}