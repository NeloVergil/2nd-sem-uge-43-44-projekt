namespace PinkScoutClasslib;

public class ActivityRepository /*: IActivityRepository*/
{
    private List<Activity> _activities;

    public ActivityRepository(List<Activity> activities)
    {
        _activities = activities;
    }

    public void CreateActivity(string id, string title, string description, string dayOfWeek, string timeSlot, int minimumAge, int maximumParticipantCount)
    {
        Activity activity =
            new Activity(id, title, description, dayOfWeek, timeSlot, minimumAge, maximumParticipantCount);
        _activities.Add(activity);
    }

    public Activity ReadActivity(string title)
    {
        return _activities.FirstOrDefault(a => a.Title == title);
    }

    public void UpdateActivity(string id, string title, string description, string dayOfWeek, string timeSlot, int minimumAge, int maximumParticipantCount)
    
    {
        Activity activity = _activities.FirstOrDefault(a => a.Title == title);
        if (activity != null)
        {
            activity.Description = description;
            activity.DayOfWeek = dayOfWeek;
            activity.TimeSlot = timeSlot;
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
    public List<Activity> GetAllActivities()
    {
        return _activities;
    }
}




