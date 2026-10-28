using Microsoft.Data.SqlClient;
namespace PinkScoutClasslib;

public class ActivityRepository : Repository
{

    public ActivityRepository(string connectionString)
        : base(connectionString)
    {
    }
    private readonly string _connectionString;

    public Activity CreateActivity(Activity newActivity)
    {
        string Sql =
            "insert into Activity (Name, Description, DayOfWeek, TimeSlot, MinimumAge, MaxParticipantNumber) output inserted.Id values (@Name, @Description, @DayOfWeek, @TimeSlot, @MinimumAge, @MaxParticipantNumber)";
        using (SqlConnection connection = CreateConnection())
        {
            using (SqlCommand command = new SqlCommand(Sql, connection))
            {
                command.Parameters.AddWithValue("@Name", newActivity.Name);
                command.Parameters.AddWithValue("@Description", newActivity.Description);
                command.Parameters.AddWithValue("@DayOfWeek", newActivity.DayOfWeek);
                command.Parameters.AddWithValue("@TimeSlot", newActivity.TimeSlot);
                command.Parameters.AddWithValue("@MinimumAge", newActivity.MinimumAge);
                command.Parameters.AddWithValue("@MaxParticipantNumber", newActivity.MaxParticipantNumber);
                int id = (int) command.ExecuteScalar();
                newActivity.Id = id;
            }
        }                
        return newActivity;

    }
    public Activity? ReadActivity(int id)
    {
        string sql = "SELECT * FROM Activity WHERE Id = @Id";

        using (SqlConnection connection = CreateConnection())
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
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

    public void UpdateActivity(Activity updatedActivity)
    {
        string Sql =
        "insert into Activity (Name, Description, DayOfWeek, Timeslot, MinimumAge, MaximumParticipantNumber) values (@Name, @Description, @DayOfWeek, @Timeslot, @MinimumAge, @MaximumParticipantCount)";

        using (SqlConnection connection = CreateConnection())

        using (SqlCommand command = new SqlCommand(Sql, connection))
        {
            command.Parameters.AddWithValue("@Name", updatedActivity.Name);
            command.Parameters.AddWithValue("@Description", updatedActivity.Description);
            command.Parameters.AddWithValue("@DayOfWeek", updatedActivity.DayOfWeek);
            command.Parameters.AddWithValue("@Timeslot", updatedActivity.TimeSlot);
            command.Parameters.AddWithValue("@MinimumAge", updatedActivity.MinimumAge);
            command.Parameters.AddWithValue("@MaximumParticipantCount", updatedActivity.MaxParticipantNumber);
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

