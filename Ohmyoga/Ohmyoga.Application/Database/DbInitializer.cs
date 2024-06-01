using Dapper;

namespace Ohmyoga.Application.Database;

public class DbInitializer
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DbInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync("""
                                      create table if not exists courses (
                                          id UUID primary key,
                                          title TEXT not null,
                                          slug TEXT not null,
                                          date TEXT not null,
                                          capacity integer not null,
                                          duration integer not null
                                      );
                                      """);
    }
}