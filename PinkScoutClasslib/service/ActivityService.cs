
namespace PinkScoutClasslib;

public class ActivityService
{
    private ActivityRepository _activityRepository;

    public ActivityService(ActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }
    public List<Activity> GetWeeklySchedule()
    {
        return _activityRepository.GetAllActivities();
    }
    public Activity GetActivityByTitle(string title)
    {
        return _activityRepository.ReadActivity(title);
    }

    public void RegisterUser(User user, Activity activity)
    {
     
    }

    public void UnregisterUser(User user, Activity activity)
    {

    }
}

