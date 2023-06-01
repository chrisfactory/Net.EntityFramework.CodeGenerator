namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IDotNetProjectFileInfoFactory
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
    internal class DotNetProjectFileInfoFactory : IDotNetProjectFileInfoFactory
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

}
