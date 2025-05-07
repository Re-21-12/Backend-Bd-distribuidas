using api_db.Data;
using Microsoft.EntityFrameworkCore;

public class AppDbContextFactory
{
    private readonly IConfiguration _config;
    private readonly IReadOnlyConnectionSelector _selector;

    public AppDbContextFactory(IConfiguration config, IReadOnlyConnectionSelector selector)
    {
        _config = config;
        _selector = selector;
    }

    public AppDbContext CreateReadOnlyContext()
    {
        var conn = _selector.GetNextReadOnlyConnection();
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));
        return new AppDbContext(optionsBuilder.Options);
    }

    public AppDbContext CreateWriteContext()
    {
        var conn = _config.GetConnectionString("Master");
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));
        return new AppDbContext(optionsBuilder.Options);
    }
}
