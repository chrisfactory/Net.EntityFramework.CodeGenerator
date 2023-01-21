namespace SqlG
{
    internal class EntityStrategy : IEntityStrategy
    {
        public EntityStrategy(IEntitySchemaProvider provider ,IEnumerable<IEntityMapGenerator> entityMapGens)
        {
            var mapGen = entityMapGens.ToList();

            foreach (var item in mapGen)
            {
                item.Generate();
            }
        }
    }
}
