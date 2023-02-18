namespace SqlG
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        public static ISqlGenEntityBuilder Delete(this ISqlGenEntityBuilder genBuilder, string name, Action<ISpDeleteBuilder>? builder = null)
        {
            return genBuilder.AddGenActionBuilder<ISpDeleteBuilder, SpDeleteBuilder>(builder);
        }
    }
}
