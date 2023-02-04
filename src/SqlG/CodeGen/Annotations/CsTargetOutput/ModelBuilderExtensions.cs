using SqlG;

namespace Microsoft.EntityFrameworkCore
{
    public static partial class ModelBuilderExtensions
    {
        public static ModelBuilder HasDefaultCsTargetOutput(this ModelBuilder modelBuilder, string rootPath)
        {
            return modelBuilder.HasDefaultCsTargetOutput(o =>
            {
                o.RootPath = rootPath;
            });
        }
        public static ModelBuilder HasDefaultCsTargetOutput(this ModelBuilder modelBuilder, Action<CsTargetOutput> config)
        {
            var option = new CsTargetOutput();
            config(option);
            modelBuilder.HasAnnotation(CsTargetOutput.AnnotationKey, option);
            return modelBuilder;
        }
    }
}
