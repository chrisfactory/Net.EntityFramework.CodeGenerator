namespace SqlG
{
    public interface IEntitySchema
    {
        IEnumerable<IEntityColumn> Columns { get; }
    }

    public interface IEntityColumn
    {
        public Type Type { get; }
        public bool Riquierd { get; }
        public int? Order { get; }
        public string ModelName { get; }
        public string DbName { get; }     }


}
