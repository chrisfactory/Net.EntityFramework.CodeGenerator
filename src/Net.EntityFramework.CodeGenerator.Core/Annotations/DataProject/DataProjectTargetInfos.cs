namespace Microsoft.Extensions.DependencyInjection
{
    public interface IDataProjectTargetInfos
    {
        string? RootPath { get; }

        string TablesPatternPath { get; }
        string StoredProceduresPatternPath { get; }
        string IndexsPatternPath { get; }
        string SequencesPatternPath { get; }
        string SchemasPatternPath { get; }
    }
    public class DataProjectOptions : IDataProjectTargetInfos
    {
        public const string AnnotationKey = $"sqlg.{nameof(DataProjectOptions)}";

        public string? RootPath { get; set; }

        /// <summary>
        /// {FileName};{Schema};{SchemaExt};{TableName}
        /// </summary>
        public string TablesPatternPath { get; set; } = @"{Schema}\Tables\{FileName}.sql";
        /// <summary>
        /// {FileName};{Schema};{SchemaExt};{TableName}
        /// </summary>
        public string IndexsPatternPath { get; set; } = @"{Schema}\Indexs\{TableName}\{FileName}.sql";

        /// <summary>
        /// {FileName};{Schema};{SchemaExt};{SequenceName}
        /// </summary>
        public string SequencesPatternPath { get; set; } = @"{Schema}\Sequences\{FileName}.sql";
      
        /// <summary>
        /// {FileName};{Schema};{SchemaExt}
        /// </summary>
        public string SchemasPatternPath { get; set; } = @"Schemas\{FileName}.sql";

        /// <summary>
        /// {FileName};{Schema};{SchemaExt};{TableName};{StoredProcedureName}
        /// </summary>
        public string StoredProceduresPatternPath { get; set; } = @"{Schema}\Stored Procedures\{TableName}\{FileName}.sql";

        

    }
}
