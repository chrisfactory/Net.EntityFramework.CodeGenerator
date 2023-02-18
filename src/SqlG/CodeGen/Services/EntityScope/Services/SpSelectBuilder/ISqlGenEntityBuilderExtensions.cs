namespace SqlG
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        public static ISqlGenEntityBuilder Select(this ISqlGenEntityBuilder genBuilder, string name, Action<ISpSelectBuilder>? builder = null)
        {
            return genBuilder.AddGenActionBuilder<ISpSelectBuilder, SpSelectBuilder>(builder);
        }
    }
}
