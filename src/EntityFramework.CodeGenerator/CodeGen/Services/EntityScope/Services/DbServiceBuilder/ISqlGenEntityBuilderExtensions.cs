namespace EntityFramework.CodeGenerator
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        public static ISqlGenEntityBuilder DbService(this ISqlGenEntityBuilder genBuilder, string name, Action<IDbServiceBuilder>? builder = null)
        {
            return genBuilder.AddGenActionBuilder<IDbServiceBuilder, DbServiceBuilder>(builder);
        }
    }
}
