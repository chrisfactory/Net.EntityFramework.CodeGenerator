namespace SqlG
{
     
    internal class FixedSpInsertNameProvider : ISpNameProvider
    {
        private readonly string _name;
        public FixedSpInsertNameProvider(string name)
        {
            _name = name;
        }
        public string Get()
        {
            return _name;
        }
    }
    internal class SpInsertNameProvider : ISpNameProvider
    {
        private readonly IEntityTypeTable _entity;
        public SpInsertNameProvider(IEntityTypeTable entity)
        {
            _entity = entity;
        }
        public string Get()
        {
            return $"Insert{_entity.Table.Name}";
        }
    }
}
