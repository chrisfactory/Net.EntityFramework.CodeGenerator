using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ModelBuilderExtensions
    {
        public static ModelBuilder HasDotNetProject(this ModelBuilder modelBuilder, string rootPath)
        {
            return modelBuilder.HasDotNetProject(o =>
            {
                o.RootPath = rootPath;
            });
        }
        public static ModelBuilder HasDotNetProject(this ModelBuilder modelBuilder, Action<DotNetProjectOptions> config)
        {
            var option = new DotNetProjectOptions();
            config(option);
            modelBuilder.HasAnnotation(DotNetProjectOptions.AnnotationKey, option);
            return modelBuilder;
        }
    }
}
