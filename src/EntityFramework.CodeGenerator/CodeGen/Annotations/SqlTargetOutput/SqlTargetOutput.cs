namespace EntityFramework.CodeGenerator
{
    public interface ISqlTargetOutput
    {
        string? RootPath { get; }

        string TablesPatternPath { get; }
        string StoredProceduresPatternPath { get; }
        string IndexsPatternPath { get; }
        string SequencesPatternPath { get; }
        string SchemasPatternPath { get; }
    }
    public class SqlTargetOutput : ISqlTargetOutput
    {
        public const string AnnotationKey = $"sqlg.{nameof(SqlTargetOutput)}";

        public string? RootPath { get; set; }

        public string TablesPatternPath { get; set; } = @"{schema}\Tables\{name}.sql";
        public string StoredProceduresPatternPath { get; set; } = @"{schema}\Stored Procedures\{name}\{spname}.sql";
        public string IndexsPatternPath { get; set; } = @"{schema}\Indexs\{name}.sql";
        public string SequencesPatternPath { get; set; } = @"{schema}\Sequences\{name}.sql";
        public string SchemasPatternPath { get; set; } = @"Schemas\{name}.sql";
    }
}
