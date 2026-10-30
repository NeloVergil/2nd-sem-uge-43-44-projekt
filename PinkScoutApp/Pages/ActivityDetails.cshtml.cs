using Microsoft.AspNetCore.Mvc.RazorPages;
using PinkScoutClasslib;
namespace PinkScoutApp.Pages;

public class ActivityDetailsModel : PageModel
{
    private readonly ActivityService _activityService;
    
    public Activity Activity { get; set; }
    
    public ActivityDetailsModel(ActivityService ActivityService)
    {
        _activityService = ActivityService;
    }
    public Activity CurrentActivity { get; set; }
    
    
    public void OnGet(int id)
    {
        CurrentActivity = _activityService.GetActivity(id);
    }
    
}