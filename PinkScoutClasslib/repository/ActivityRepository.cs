namespace PinkScoutClasslib;

public class ActivityRepository /*: IActivityRepository*/
{
    private List<Activity> _activities;

    public ActivityRepository(List<Activity> activities)
    {
        _activities = activities;
    }

    public void CreateActivity(string title, string description, DateTime starttime, DateTime endtime, int minimumAge, int maximumParticipantCount)
    {
        Activity activity = new Activity(title, description, starttime, endtime, minimumAge, maximumParticipantCount);
        _activities.Add(activity);
    }

    public Activity ReadActivity(string title)
    {
        return _activities.FirstOrDefault(a => a.Title == title);
    }

    public void UpdateActivity(string title, string description, DateTime starttime, DateTime endtime, int minimumAge, int maximumParticipantCount)
    {
        Activity activity = _activities.FirstOrDefault(a => a.Title == title);
        if (activity != null)
        {
            activity.Description = description;
            activity.starttime = starttime;
            activity.endtime = endtime;
            activity.minimumAge = minimumAge;
            activity.maximumParticipantCount = maximumParticipantCount;
        }
    }

        public void DeleteActivity(string title)
    {
        Activity activity = _activities.FirstOrDefault(a => a.Title == title);
        if (activity != null)
        {
            _activities.Remove(activity);
        }

    
    }
}




