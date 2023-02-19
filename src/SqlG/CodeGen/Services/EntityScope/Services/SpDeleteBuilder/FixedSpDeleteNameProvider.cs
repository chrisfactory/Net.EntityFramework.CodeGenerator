namespace SqlG
{
     
    internal class FixedSpDeleteNameProvider : ISpNameProvider
    {
        private readonly string _name;
        public FixedSpDeleteNameProvider(string name)
        {
            _name = name;
        }
        public string Get()
        {
            return _name;
        }
    }
    internal class SpDeleteNameProvider : ISpNameProvider
    {
        private readonly IEntityTypeTable _entity;
        public SpDeleteNameProvider(IEntityTypeTable entity)
        {
            _entity = entity;
        }
        public string Get()
        {
            return $"Delete{_entity.Table.Name}";
        }
    }
}
