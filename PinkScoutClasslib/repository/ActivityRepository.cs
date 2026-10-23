namespace PinkScoutClasslib;

public class ActivityRepository : IActivityRepository
{
    private List<Activity> _activities;

    public ActivityRepository(Activity activities)
    {
        _activities = activities;
    }

    public CreateActivity(string title, string description, DateTime starttime, DateTime endtime, int minimumAge, int maximumParticipantCount)
    {
        string activity = new Activity(title, description, starttime, endtime, minimumAge, maximumParticipantCount);
        _activities.Add(activity);
    }

    public ReadActivity(string title)
    {
        return _activities.FirstOrDefault(a => a.Title == title);
    }

    public UpdateActivity(string title, string description, DateTime starttime, DateTime endtime, int minimumAge, int maximumParticipantCount)
    {
        string activity = _activities.FirstOrDefault(a => a.Title == title);
        if (activity != null)
        {
            activity.Description = description;
            activity.starttime = starttime;
            activity.endtime = endtime;
            activity.minimumAge = minimumAge;
            activity.maximumParticipantCount = maximumParticipantCount;
        }
    }

        public DeleteActivity(string title)
    {
        string activity = _activities.FirstOrDefault(a => a.Title == title);
        if (activity != null)
        {
            _activities.Remove(activity);
        }

    
    }
}




