using Npgsql;

namespace BookQuotesApp.Api.Data;

/// <summary>
/// Accepts either a plain Npgsql keyword string or a "postgres(ql)://" URL
/// (the format Neon and Render hand out) and always returns a keyword string.
/// Lets the same configuration value work locally (User Secrets) and in Render (env var).
/// </summary>
public static class DatabaseConnection
{
    public static string Resolve(string? configured)
    {
        if (string.IsNullOrWhiteSpace(configured))
            throw new InvalidOperationException(
                "No database connection string configured. Set 'ConnectionStrings:Default' " +
                "(User Secrets locally, environment variable in production).");

        var value = configured.Trim();

        var isUrl = value.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase)
                    || value.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase);
        if (!isUrl)
            return value;

        var uri = new Uri(value);
        var userInfo = uri.UserInfo.Split(':', 2);

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.IsDefaultPort ? 5432 : uri.Port,
            Database = Uri.UnescapeDataString(uri.AbsolutePath.TrimStart('/')),
            Username = Uri.UnescapeDataString(userInfo[0]),
            Password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : null,
            SslMode = SslMode.Require,
        };

        return builder.ConnectionString;
    }
}
