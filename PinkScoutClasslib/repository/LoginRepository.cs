using System.Security.Cryptography;
using Microsoft.Data.SqlClient;

namespace PinkScoutClasslib;

public sealed class LoginRepository
{
    private const int HashIterations = 100_000;
    private readonly string _connectionString;

    public LoginRepository(string connectionString)
    {
        _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new ArgumentException("A database connection string is required.", nameof(connectionString))
            : connectionString;
    }

    public async Task<bool> CreateUserAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        var passwordHash = HashPassword(password);

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = """
            IF EXISTS (SELECT 1 FROM UserAuth WHERE UserName = @UserName)
                SELECT CAST(0 AS bit);
            ELSE
            BEGIN
                INSERT INTO UserAuth (UserName, PasswordHash, IsLeader)
                VALUES (@UserName, @PasswordHash, 0);
                SELECT CAST(1 AS bit);
            END
            """;

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@UserName", username);
        command.Parameters.AddWithValue("@PasswordHash", passwordHash);

        return (bool)(await command.ExecuteScalarAsync(cancellationToken)
            ?? throw new InvalidOperationException("Creating the user returned no result."));
    }

    public async Task<bool> ValidateCredentialsAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = """
            SELECT PasswordHash
            FROM UserAuth
            WHERE UserName = @UserName
            """;

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@UserName", username);
        var storedHash = await command.ExecuteScalarAsync(cancellationToken) as string;

        return storedHash is not null && VerifyPassword(password, storedHash);
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
}
