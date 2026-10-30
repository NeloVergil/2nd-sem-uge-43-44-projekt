namespace PinkScoutClasslib;

public class ActivityService
{
    private readonly ActivityRepository _activityRepository;
    private readonly LoginRepository _loginRepository;

    public ActivityService(
        ActivityRepository activityRepository,
        LoginRepository loginRepository)
    {
        _activityRepository = activityRepository;
        _loginRepository = loginRepository;
    }


    public void RegisterUser(User user, Activity activity)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(activity);

        if (_activityRepository.RegisterUser(user, activity) &&
            !activity.Users.Any(registeredUser => registeredUser.Id == user.Id))
        {
            activity.Users.Add(user);
        }
    }

    public void UnregisterUser(User user, Activity activity)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(activity);

        if (_activityRepository.UnregisterUser(user, activity))
        {
            activity.Users.RemoveAll(candidate => candidate.Id == user.Id);
        }
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

    public List<Activity> GetWeeklySchedule()
    {
        return _activityRepository.GetAllActivities();
    }

    public Activity GetActivity(int id)
    {
        throw new NotImplementedException();
    }

    public List<Activity> GetActivitiesForUser(User user)
    {
        return _activityRepository.GetAllActivities()
            .Where(activity => activity.Users.Any(u => u.Id == user.Id))
            .ToList();
    }

    private List<Activity> GetAllActivities()
    {
        return _activityRepository.GetAllActivities();
    }

    private List<User> GetAllUsers()
    {
        return _loginRepository.GetAllUsers();
    }

}
