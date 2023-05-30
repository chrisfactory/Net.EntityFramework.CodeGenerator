namespace Microsoft.Extensions.DependencyInjection
{
    public interface IDotNetProjectTargetInfos
    {
        string? RootPath { get; }

        string MapExtensionsPatternPath { get; }
        string DbServicePatternPath { get; }
    }
    public class DotNetProjectOptions : IDotNetProjectTargetInfos
    {
        public const string AnnotationKey = $"sqlg.{nameof(DotNetProjectOptions)}";

        public string? RootPath { get; set; }

        public string MapExtensionsPatternPath { get; set; } = @"MapExtensions\{name}\{classname}.cs";
        public string DbServicePatternPath { get; set; } = @"DbService\{name}\{classname}.cs";

    }
}
