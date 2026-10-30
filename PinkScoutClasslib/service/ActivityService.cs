using Microsoft.Data.SqlClient;
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

    public List<User> GetUsersFromActivity(int activityId)
    {
        List<User> users = new List<User>();
    
        string sql = @"SELECT [User].*
                   FROM [User]
                   JOIN Registration ON [User].Id = Registration.UserId
                   WHERE Registration.ActivityId = @Id";
    
        using SqlConnection connection = CreateConnection();
        connection.Open();
    
        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", activityId);
    
        using SqlDataReader reader = command.ExecuteReader();
    
        while (reader.Read())
        {
            User user = new User(
                reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("Name")),
                reader.GetString(reader.GetOrdinal("PasswordHash")),
                reader.GetBoolean(reader.GetOrdinal("IsLeader")),
                reader.GetInt32(reader.GetOrdinal("Age"))
            );
    
            users.Add(user);
        }
    
        return users;
    }
}




