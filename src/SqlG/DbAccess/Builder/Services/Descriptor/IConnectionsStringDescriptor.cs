namespace DataBaseAccess
{
    public interface IConnectionsStringDescriptor
    {
        string Name { get; }

        string Build();
    }
}
