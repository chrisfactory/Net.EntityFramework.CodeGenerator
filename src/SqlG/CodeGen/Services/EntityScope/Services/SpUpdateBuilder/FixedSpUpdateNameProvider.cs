namespace SqlG
{
     
    internal class FixedSpUpdateNameProvider : ISpNameProvider
    {
        private readonly string _name;
        public FixedSpUpdateNameProvider(string name)
        {
            _name = name;
        }
        public string Get()
        {
            return _name;
        }
    }
    internal class SpUpdateNameProvider : ISpNameProvider
    {
        private readonly IEntityTypeTable _entity;
        public SpUpdateNameProvider(IEntityTypeTable entity)
        {
            _entity = entity;
        }
        public string Get()
        {
            return $"Update{_entity.Table.Name}";
        }
    }
}
