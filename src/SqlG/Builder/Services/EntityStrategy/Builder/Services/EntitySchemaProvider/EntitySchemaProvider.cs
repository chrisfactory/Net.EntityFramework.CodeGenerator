namespace SqlG
{
    internal class EntitySchemaProvider : IEntitySchemaProvider
    {
        private readonly  Lazy<IEntitySchema> _schema;
        public EntitySchemaProvider(IEntitySchemaFactory factory)
        {
            _schema = new Lazy<IEntitySchema>(factory.Create);
        }
     
        public IEntitySchema Get()
        {
            return _schema.Value;
        }
    }
}
