using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PinkScoutClasslib;
namespace PinkScoutApp.Pages;

[Authorize(Roles = "leader")]
public class LeaderCalendarModel : PageModel
{
    private readonly ActivityService _activityService;
    private readonly ActivityRepository _activityRepository;

    public LeaderCalendarModel(ActivityService activityService, ActivityRepository activityRepository)
    {
        _activityService = activityService;
        _activityRepository = activityRepository;
    }

    public List<string> Days { get; } = new List<string> { "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "Lørdag", "Søndag" };
    public List<string> TimeSlots { get; } = new List<string> { "Formiddag", "Eftermiddag", "Aften" };

    // Vi mapper nu til din rigtige <Activity> model
    public Dictionary<(string TimeSlot, string Day), Activity> Schedule { get; set; }

    public void OnGet()
    {
        LoadSchedule();
    }

    public IActionResult OnPostCreateActivity(string name, string description, string dayOfWeek, string timeSlot, int minimumAge, int maxParticipantNumber)
    {
        Activity newActivity = new Activity(
            name,
            description,
            dayOfWeek,
            timeSlot,
            minimumAge,
            maxParticipantNumber);

        try
        {
            _activityRepository.CreateActivity(newActivity);
        }
        catch (InvalidOperationException exception)
        {
            ModelState.AddModelError(
                string.Empty,
                exception.Message);
            LoadSchedule();
            return Page();
        }

        return RedirectToPage();
    }

    private void LoadSchedule()
    {
        var activities = _activityService.GetWeeklySchedule();
        Schedule = activities
            .GroupBy(activity => (activity.TimeSlot, activity.DayOfWeek))
            .ToDictionary(group => group.Key, group => group.First());
    }

    public IActionResult OnPostRegister(int activityId)
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdValue, out var userId))
            return Challenge();

        var user = new User(userId, User.Identity?.Name ?? string.Empty, true, 0);
        var activity = _activityRepository.ReadActivity(activityId);
        if (activity is null)
            return NotFound();

        try
        {
            _activityService.RegisterUser(user, activity);
        }
        catch (InvalidOperationException exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            LoadSchedule();
            return Page();
        }

        return RedirectToPage();
    }
}