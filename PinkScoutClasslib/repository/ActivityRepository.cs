using Microsoft.Data.SqlClient;
namespace PinkScoutClasslib;

public class ActivityRepository : Repository
{

    public ActivityRepository(string connectionString)
        : base(connectionString)
    {
    }
    private readonly string _connectionString;

    public void Create(Activity newActivity)
    {
        string Sql =
            "insert into Activity ( Id, Name, Description, DayOfWeek, TimeSlot, MinimumAge, MaxParticipantNumber) values (@Id, @Name, @Description, @DayOfWeek, @TimeSlot, @MinimumAge, @MaxParticipantNumber)";
        using (SqlConnection connection = CreateConnection())
        {
            using (SqlCommand command = new SqlCommand(Sql, connection))
            {
                command.Parameters.AddWithValue("@Id", newActivity.Id);
                command.Parameters.AddWithValue("@Name", newActivity.Name);
                command.Parameters.AddWithValue("@Description", newActivity.Description);
                command.Parameters.AddWithValue("@DayOfWeek", newActivity.DayOfWeek);
                command.Parameters.AddWithValue("@TimeSlot", newActivity.TimeSlot);
                command.Parameters.AddWithValue("@MinimumAge", newActivity.MinimumAge);
                command.Parameters.AddWithValue("@MaxParticipantNumber", newActivity.MaxParticipantNumber);
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
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetString(reader.GetOrdinal("DayOfWeek")),
                            reader.GetString(reader.GetOrdinal("TimeSlot")),
                            reader.GetInt32(reader.GetOrdinal("MinimumAge")),
                            reader.GetInt32(reader.GetOrdinal("MaxParticipantNumber"))
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

    public void UpdateActivity(Activity updatedactivity)
    {
        string Sql =
        "insert into Activity (id, title, description, dayOfWeek, timeslot, minimumAge, maximumParticipantCount) values (@id, @title, @description, @dayOfWeek, @timeslot, @minimumAge, @maximumParticipantCount)";

        using (SqlConnection connection = UpdateActivity())

        using (SqlCommand command = new SqlCommand(Sql, connection))
        {
            command.Parameters.AddWithValue("@id", newactivity.Id);
            command.Parameters.AddWithValue("@title", newactivity.Title);
            command.Parameters.AddWithValue("@description", newactivity.Description);
            command.Parameters.AddWithValue("@dayOfWeek", newactivity.DayOfWeek);
            command.Parameters.AddWithValue("@timeslot", newactivity.TimeSlot);
            command.Parameters.AddWithValue("@minimumAge", newactivity.minimumAge);
            command.Parameters.AddWithValue("@maximumParticipantCount", newactivity.maximumParticipantCount);
            command.ExecuteNonQuery();
        }

    }

    public void DeleteActivity(int id)
    {
        string sql = "DELETE FROM Activity WHERE Id = @Id";

        using (SqlConnection connection = CreateConnection())
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }

    public List<Activity> GetAllActivities()
    {
        List<Activity> activities = new List<Activity>();

        return activities;
    }


}

