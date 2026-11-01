using Microsoft.Data.SqlClient;
namespace PinkScoutClasslib;

public class ActivityRepository : Repository
{

    public ActivityRepository(string connectionString)
        : base(connectionString)
    {
    }
    public Activity CreateActivity(Activity newActivity)
    {
        const string sql = """
            IF EXISTS (
                SELECT 1
                FROM Activity
                WHERE DayOfWeek = @DayOfWeek AND TimeSlot = @TimeSlot
            )
                THROW 50001, 'An activity already exists at this day and time slot.', 1;

            INSERT INTO Activity
                (Name, Description, DayOfWeek, TimeSlot, MinimumAge, MaxParticipantNumber)
            OUTPUT INSERTED.Id
            VALUES
                (@Name, @Description, @DayOfWeek, @TimeSlot, @MinimumAge, @MaxParticipantNumber);
            """;

        using (SqlConnection connection = CreateConnection())
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Name", newActivity.Name);
                command.Parameters.AddWithValue("@Description", newActivity.Description);
                command.Parameters.AddWithValue("@DayOfWeek", newActivity.DayOfWeek);
                command.Parameters.AddWithValue("@TimeSlot", newActivity.TimeSlot);
                command.Parameters.AddWithValue("@MinimumAge", newActivity.MinimumAge);
                command.Parameters.AddWithValue(
                    "@MaxParticipantNumber",
                    newActivity.MaxParticipantNumber);

                try
                {
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    newActivity.Id = id;
                }
                catch (SqlException exception) when (exception.Number == 50001)
                {
                    throw new InvalidOperationException(
                        "An activity already exists at this day and time slot.",
                        exception);
                }
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

    public bool RegisterUser(User user, Activity activity)
    {
        const string sql = """
            IF EXISTS (
                SELECT 1
                FROM UserActivity
                WHERE UserId = @UserId AND ActivityId = @ActivityId
            )
                SELECT 0;
            ELSE IF (
                SELECT COUNT(*)
                FROM UserActivity
                WHERE ActivityId = @ActivityId
            ) >= @MaxParticipantNumber
                SELECT -1;
            ELSE
            BEGIN
                INSERT INTO UserActivity (UserId, ActivityId)
                VALUES (@UserId, @ActivityId);
                SELECT 1;
            END
            """;

        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@UserId", user.Id);
        command.Parameters.AddWithValue("@ActivityId", activity.Id);
        command.Parameters.AddWithValue(
            "@MaxParticipantNumber",
            activity.MaxParticipantNumber);

        int result = Convert.ToInt32(command.ExecuteScalar());
        if (result == -1)
        {
            throw new InvalidOperationException(
                "The activity has reached its maximum participant count.");
        }

        return result == 1;
    }

    public bool UnregisterUser(User user, Activity activity)
    {
        const string sql = """
            DELETE FROM UserActivity
            WHERE UserId = @UserId AND ActivityId = @ActivityId
            """;

        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@UserId", user.Id);
        command.Parameters.AddWithValue("@ActivityId", activity.Id);

        return command.ExecuteNonQuery() > 0;
    }

    public void UpdateActivity(Activity updatedActivity)
    {
        string Sql =
        "insert into Activity (Name, Description, DayOfWeek, Timeslot, MinimumAge, MaximumParticipantNumber) values (@Name, @Description, @DayOfWeek, @Timeslot, @MinimumAge, @MaximumParticipantCount)";

        using (SqlConnection connection = CreateConnection())

        using (SqlCommand command = new SqlCommand(Sql, connection))
        {
            connection.Open();

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
        const string sql = """
            SELECT Id, Name, Description, DayOfWeek, TimeSlot,
                   MinimumAge, MaxParticipantNumber
            FROM Activity
            """;

        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new SqlCommand(sql, connection);
        using SqlDataReader reader = command.ExecuteReader();

        List<Activity> activities = new List<Activity>();
        while (reader.Read())
        {
            activities.Add(new Activity(
                reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("Name")),
                reader.GetString(reader.GetOrdinal("Description")),
                reader.GetString(reader.GetOrdinal("DayOfWeek")),
                reader.GetString(reader.GetOrdinal("TimeSlot")),
                reader.GetInt32(reader.GetOrdinal("MinimumAge")),
                reader.GetInt32(reader.GetOrdinal("MaxParticipantNumber"))));
        }

        return activities;
    }

}
