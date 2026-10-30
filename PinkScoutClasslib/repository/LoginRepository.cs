using System.Data;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;

namespace PinkScoutClasslib;

public sealed class LoginRepository : Repository
{
    private const int HashIterations = 100_000;

    public LoginRepository(string connectionString)
        : base(connectionString)
    {
    }

    public async Task<User?> CreateUserAsync(
        string username,
        string password,
        bool isLeader,
        CancellationToken cancellationToken = default)
    {
        var passwordHash = HashPassword(password);

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);

        const string sql = """
            IF EXISTS (SELECT 1 FROM UserAuth WHERE UserName = @UserName)
                RETURN;

            INSERT INTO UserAuth (UserName, PasswordHash, IsLeader)
            OUTPUT INSERTED.Id, INSERTED.UserName, INSERTED.PasswordHash, INSERTED.IsLeader
            VALUES (@UserName, @PasswordHash, @IsLeader);
            """;

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@UserName", username);
        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
        command.Parameters.AddWithValue("@IsLeader", isLeader);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken)
            ? ReadUser(reader)
            : null;
    }

    public async Task<User?> ValidateCredentialsAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);

        const string sql = """
            SELECT Id, UserName, PasswordHash, IsLeader
            FROM UserAuth
            WHERE UserName = @UserName
            """;

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@UserName", username);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
            return null;

        var user = ReadUser(reader);
        return VerifyPassword(password, user.PasswordHash) ? user : null;
    }

    private static User ReadUser(SqlDataReader reader)
    {
        return new User(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetBoolean(3),
            0);
    }

    private static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            HashIterations,
            HashAlgorithmName.SHA256,
            32);

        return $"PBKDF2-SHA256:{HashIterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 4 ||
            parts[0] != "PBKDF2-SHA256" ||
            !int.TryParse(parts[1], out var iterations))
        {
            return false;
        }

        try
        {
            var salt = Convert.FromBase64String(parts[2]);
            var expectedHash = Convert.FromBase64String(parts[3]);
            var actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public User? ReadUser(int id)
    {
        string sql = "SELECT * FROM User WHERE id = @Id";
        using (SqlConnection connection = CreateConnection())
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User user = new User(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("UserName")),
                            reader.GetBoolean(reader.GetOrdinal("IsLeader")),
                            reader.GetInt32(reader.GetOrdinal("Age"))
                        );

                        return user;

                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }

    public List<User> GetAllUsers()
    {
        List<User> users = new List<User>();

        string sql = "SELECT * FROM User";

        using (SqlConnection connection = CreateConnection())
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("UserName")),
                            reader.GetBoolean(reader.GetOrdinal("IsLeader")),
                            reader.GetInt32(reader.GetOrdinal("Age"))
                        );

                        users.Add(user);
                    }
                }
            }
        }

        return users;
    }



}
