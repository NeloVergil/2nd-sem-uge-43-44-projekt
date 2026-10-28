using Microsoft.Data.SqlClient;
namespace PinkScoutClasslib;

public class ActivityRepository : Repository
{
    
    public ActivityRepository(string connectionString)
        : base(connectionString)
    {
    }
    private readonly string _connectionString;

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
                // command.Parameters.AddWithValue("@description", newactivity.Description);
                // command.Parameters.AddWithValue("@dayOfWeek", newactivity.DayOfWeek);
                // command.Parameters.AddWithValue("@timeslot", newactivity.TimeSlot);
                // command.Parameters.AddWithValue("@minimumAge", newactivity.minimumAge);
                // command.Parameters.AddWithValue("@maximumParticipantCount", newactivity.maximumParticipantCount);
                command.ExecuteNonQuery();
            }
        }
    }
    public Activity? ReadActivity(int id)
    {
        string sql = "SELECT * FROM Activity WHERE Id = @Id";

        using (SqlConnection connection = CreateConnection())
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Activity activity = new Activity(
                            id: reader.GetInt32(reader.GetOrdinal("Id")),
                            title: reader.GetString(reader.GetOrdinal("Title")),
                            description: reader.GetString(reader.GetOrdinal("Description")),
                            dayOfWeek: reader.GetString(reader.GetOrdinal("DayOfWeek")),
                            timeSlot: reader.GetString(reader.GetOrdinal("TimeSlot")),
                            minimumAge: reader.GetInt32(reader.GetOrdinal("MinimumAge")),
                            maximumParticipantCount: reader.GetInt32(reader.GetOrdinal("MaximumParticipantCount"))
                        );

                        return activity;

                    }
                        else
                        {
                            return null;
                        }
                }
            }
        }
    }


    public List<Activity> GetAllActivities()
    {
        return ;
    }
    
    
}




