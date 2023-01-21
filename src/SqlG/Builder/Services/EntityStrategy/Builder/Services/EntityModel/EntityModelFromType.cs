namespace SqlG
{ 
    public class EntityModelFromType<TEntity> : IEntityModel
    {
        public EntityModelFromType()
        {
            ModelType = typeof(TEntity); 
        
        }

        public Type ModelType { get; }
    }
}
