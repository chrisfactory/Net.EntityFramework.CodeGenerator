namespace SqlG
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        public static ISqlGenEntityBuilder Update(this ISqlGenEntityBuilder genBuilder, string name, Action<ISpUpdateBuilder>? builder = null)
        {
            return genBuilder.AddGenActionBuilder<ISpUpdateBuilder, SpUpdateBuilder>(builder);
        }
    }
}
