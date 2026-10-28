
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
    public Activity GetActivityByName(string name)
    {
        foreach (Activity activity in _activityRepository.GetAllActivities())
        {
            if (name == activity.Name)
            {
                return _activityRepository.ReadActivity(activity.Id);
            }
        }
        return null;
        
    }

    public void RegisterUser(User user, Activity activity)
    {
        

    }

    public void UnregisterUser(User user, Activity activity)
    {

    }

    public Activity GetActivity(int id)
    {
        throw new NotImplementedException();
    }

    public List<Activity> GetActivitiesForUser(User user)
    {
        return new List<Activity>();
    }

}

