using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ModelBuilderExtensions
    {
        public static ModelBuilder HasDataProject(this ModelBuilder modelBuilder, string rootPath)
        {
            return modelBuilder.HasDataProject(o =>
            {
                o.RootPath = rootPath;
            });
        }
        public static ModelBuilder HasDataProject(this ModelBuilder modelBuilder, Action<DataProjectOptions> config)
        {
            var option = new DataProjectOptions();
            config(option);
            modelBuilder.HasAnnotation(DataProjectOptions.AnnotationKey, option);
            return modelBuilder;
        }
    }
}
