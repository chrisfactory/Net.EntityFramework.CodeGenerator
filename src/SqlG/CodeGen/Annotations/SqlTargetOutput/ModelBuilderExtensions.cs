using SqlG;

namespace Microsoft.EntityFrameworkCore
{
    public static partial class ModelBuilderExtensions
    {
        public static ModelBuilder HasDefaultSqlTargetOutput(this ModelBuilder modelBuilder, string rootPath)
        {
            return modelBuilder.HasDefaultSqlTargetOutput(o =>
            {
                o.RootPath = rootPath;
            });
        }
        public static ModelBuilder HasDefaultSqlTargetOutput(this ModelBuilder modelBuilder, Action<SqlTargetOutput> config)
        {
            var option = new SqlTargetOutput();
            config(option);
            modelBuilder.HasAnnotation(SqlTargetOutput.AnnotationKey, option);
            return modelBuilder;
        }
    }
}
