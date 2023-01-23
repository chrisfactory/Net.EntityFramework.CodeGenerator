namespace DataBaseAccess
{
    internal class FixedConnectionsStringDescriptor : IConnectionsStringDescriptor
    {

        private readonly string _name;
        private readonly string _value;
        public FixedConnectionsStringDescriptor(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name => _name;
        public string Build() => _value;
    }
}
