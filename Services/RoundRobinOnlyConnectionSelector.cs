public class RoundRobinReadOnlyConnectionSelector : IReadOnlyConnectionSelector
{
    private readonly string[] _readOnlyConnections;
    private int _index = 0;
    private readonly object _lock = new();

    public RoundRobinReadOnlyConnectionSelector(IConfiguration configuration)
    {
        _readOnlyConnections = configuration.GetSection("ConnectionStrings:Slaves").Get<string[]>();
    }

public string GetNextReadOnlyConnection()
{
    lock (_lock)
    {
        var conn = _readOnlyConnections[_index];
        Console.WriteLine($"[Slave Connection] Using slave {_index + 1}: {conn}");
        _index = (_index + 1) % _readOnlyConnections.Length;
        return conn;
    }
}

}
