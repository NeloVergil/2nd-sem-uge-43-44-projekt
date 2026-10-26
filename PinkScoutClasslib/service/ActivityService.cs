
namespace PinkScoutClasslib;

public class ActivityService
{
    private ActivityRepository _activityRepository;
    private LoginRepository _loginRepository;

    public ActivityService(ActivityRepository activityRepository, LoginRepository loginRepository)
    {
        _activityRepository = activityRepository;
        _loginRepository = loginRepository;
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

    public List<Activity> GetActivitiesForUser(User user)
    {
        return new List<Activity>();
    }

}

