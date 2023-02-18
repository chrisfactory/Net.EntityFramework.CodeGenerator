namespace SqlG
{
    internal interface ISqlFileInfoFactory
    {
        FileInfo CreateFileInfo(string rootPath, string pattern, string? schema, string name, string? spName = null);
    }
    internal class GetSqlFileInfoFactory: ISqlFileInfoFactory
    {
        public FileInfo CreateFileInfo(string rootPath, string pattern, string? schema, string name, string? spName = null)
        {
            var str = new StringFormatter(pattern);
            str.Add("{schema}", string.IsNullOrWhiteSpace(schema) ? "" : schema);
            str.Add("{schemaExt}", string.IsNullOrWhiteSpace(schema) ? "" : $"{schema}.");
            str.Add("{name}", name);
            str.Add("{spname}", string.IsNullOrWhiteSpace(spName) ? "" : spName);
            var subPath = str.ToString().TrimStart('\\');
            return new FileInfo(Path.Combine(rootPath, subPath));
        }
    }
}
