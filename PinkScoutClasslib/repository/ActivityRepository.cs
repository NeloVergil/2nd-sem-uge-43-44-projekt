using Microsoft.Data.SqlClient;
namespace PinkScoutClasslib;

public class ActivityRepository : Repository
{
    
    public ActivityRepository(string connectionString)
        : base(connectionString)
    {
    }
    private readonly string _connectionString;
    
    private List<Activity> _activities;
    //
    // public ActivityRepository(List<Activity> activities)
    // {
    //     _activities = activities;
    // }

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







    public void Create(Activity newactivity)
    {
        string Sql =
            "insert into Activity ( id, title, description, dayOfWeek, timeslot, minimumAge, maximumParticipantCount) values (@id, @title, @description, @dayOfWeek, @timeslot, @minimumAge, @maximumParticipantCount)";
        using (SqlConnection connection = CreateConnection())
        {
            using (SqlCommand command = new SqlCommand(Sql, connection))
            {
                command.Parameters.AddWithValue("@id", newactivity.Id);
                command.Parameters.AddWithValue("@title", newactivity.Title);
                //command.Parameters.AddWithValue("@description", newactivity.Description);
                //command.Parameters.AddWithValue("@dayOfWeek", newactivity.DayOfWeek);
               // command.Parameters.AddWithValue("@timeslot", newactivity.TimeSlot);
               // command.Parameters.AddWithValue("@minimumAge", newactivity.minimumAge);
               // command.Parameters.AddWithValue("@maximumParticipantCount", newactivity.maximumParticipantCount);
                command.ExecuteNonQuery();
            }
        }
    }
    
    
}




