namespace SqlG
{
    public interface ICsTargetOutput
    {
        string? RootPath { get; }

        string MapExtensionsPatternPath { get; }
        string DbServicePatternPath { get; }
    }
    public class CsTargetOutput : ICsTargetOutput
    {
        public const string AnnotationKey = $"sqlg.{nameof(CsTargetOutput)}";

        public string? RootPath { get; set; }

        public string MapExtensionsPatternPath { get; set; } = @"MapExtensions\{name}\{classname}.cs";
        public string DbServicePatternPath { get; set; } = @"DbService\{name}\{classname}.cs";

    }
}
