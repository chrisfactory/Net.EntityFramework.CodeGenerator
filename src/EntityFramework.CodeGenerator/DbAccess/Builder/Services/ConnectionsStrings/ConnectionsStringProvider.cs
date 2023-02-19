namespace DataBaseAccess
{
    internal class ConnectionsStringProvider : IConnectionsStringProvider
    {
        private readonly IReadOnlyDictionary<string, IConnectionsStringDescriptor> _connectionStrings;
        public ConnectionsStringProvider(IEnumerable<IConnectionsStringDescriptor> descriptors)
        {
            _connectionStrings = descriptors.ToDictionary(d => d.Name);
        }

        public string Get(string name)
        {
            return _connectionStrings[name].Build();
        }
    }
}
