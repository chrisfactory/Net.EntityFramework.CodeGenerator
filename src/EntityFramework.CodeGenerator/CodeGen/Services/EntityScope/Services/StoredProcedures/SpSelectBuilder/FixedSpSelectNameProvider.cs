namespace EntityFramework.CodeGenerator
{

    public interface ISpNameProvider
    {
        string Get();
    }

    internal class FixedSpSelectNameProvider : ISpNameProvider
    {
        private readonly string _name;
        public FixedSpSelectNameProvider(string name)
        {
            _name = name;
        }
        public string Get()
        {
            return _name;
        }
    }
    internal class SpSelectNameProvider : ISpNameProvider
    {
        private readonly IEntityTypeTable _entity;
        public SpSelectNameProvider(IEntityTypeTable entity)
        {
            _entity = entity;
        }
        public string Get()
        {
            return $"Select{_entity.Table.Name}";
        }
    }
}
