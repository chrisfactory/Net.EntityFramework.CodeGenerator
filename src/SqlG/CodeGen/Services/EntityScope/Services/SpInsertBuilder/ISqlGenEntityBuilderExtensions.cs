namespace SqlG
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        public static ISqlGenEntityBuilder Insert(this ISqlGenEntityBuilder genBuilder, string name, Action<ISpInsertBuilder>? builder = null)
        {
            return genBuilder.AddGenActionBuilder<ISpInsertBuilder, SpInsertBuilder>(builder);
        }
    }
}
