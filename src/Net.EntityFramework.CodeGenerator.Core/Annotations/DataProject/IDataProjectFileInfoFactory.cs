namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IDataProjectFileInfoFactory
    {
        FileInfo CreateFileInfo(
            string rootPath, 
            string fileName, 
            string pattern, 
            string? schema, 
            string? tableName,
            string? sequenceName,
            string? storedProcedureName);
    }
    internal class DataProjectFileInfoFactory : IDataProjectFileInfoFactory
    {
        public FileInfo CreateFileInfo(
            string rootPath, 
            string fileName, 
            string pattern, 
            string? schema,
            string? tableName,
            string? sequenceName,
            string? storedProcedureName)
        {
            var str = new StringFormatter(pattern);
            str.Add("{Schema}", string.IsNullOrWhiteSpace(schema) ? "" : schema);
            str.Add("{SchemaExt}", string.IsNullOrWhiteSpace(schema) ? "" : $"{schema}.");
            str.Add("{TableName}", string.IsNullOrWhiteSpace(tableName) ? "" : tableName);
            str.Add("{SequenceName}", string.IsNullOrWhiteSpace(sequenceName) ? "" : sequenceName);
            str.Add("{StoredProcedureName}", string.IsNullOrWhiteSpace(storedProcedureName) ? "" : storedProcedureName);
            str.Add("{FileName}", string.IsNullOrWhiteSpace(fileName) ? "" : fileName);
            var subPath = str.ToString().TrimStart('\\');
            return new FileInfo(Path.Combine(rootPath, subPath));
        }
    }

    internal class StringFormatter
    {

        public string Str { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public StringFormatter(string p_str)
        {
            Str = p_str;
            Parameters = new Dictionary<string, object>();
        }

        public void Add(string key, object val)
        {
            Parameters.Add(key, val);
        }

        public override string ToString()
        {
            return Parameters.Aggregate(Str, (current, parameter) => current.Replace(parameter.Key, parameter.Value?.ToString()));
        }

    }
}
