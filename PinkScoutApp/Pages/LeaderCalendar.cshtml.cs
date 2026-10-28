using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PinkScoutClasslib;
namespace PinkScoutApp.Pages;

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
        var activities = _activityService.GetWeeklySchedule();

        // 2. Sorter dem ind i ugeskemaet ud fra de egenskaber jeres Activity model har
        // Bemærk: Det kræver at jeres Activity har public properties for DayOfWeek og TimeSlot (f.eks. med store/små bogstaver)
        Schedule = activities.ToDictionary(a => (a.TimeSlot, a.DayOfWeek));
    }

    public IActionResult OnPostCreateActivity(string title, string description, string dayOfWeek, string timeSlot, int minimumAge, int maximumParticipantCount)
    {
        var newActivity = new Activity("", title, description, dayOfWeek, timeSlot, minimumAge, maximumParticipantCount);

        _activityRepository.CreateActivity(newActivity.Id, newActivity.Title, newActivity.Description, newActivity.DayOfWeek, newActivity.TimeSlot, newActivity.minimumAge, newActivity.maximumParticipantCount);

        return RedirectToPage();
    }
}