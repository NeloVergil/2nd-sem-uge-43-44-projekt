using Microsoft.Data.SqlClient;

namespace PinkScoutClasslib;

public abstract class Repository
{
    private readonly string _connectionString;

    protected Repository(string connectionString)
    {
        _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new ArgumentException("A database connection string is required.", nameof(connectionString))
            : connectionString;
    }

    protected SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}