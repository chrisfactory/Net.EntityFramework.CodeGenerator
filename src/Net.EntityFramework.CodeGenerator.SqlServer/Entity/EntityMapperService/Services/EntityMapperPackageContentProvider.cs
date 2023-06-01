using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Net.EntityFramework.CodeGenerator.Core;
using System.Data;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EntityMapperPackageContentProvider : IIntentContentProvider
    {

        private readonly IEntityMapperCodeGeneratorSource _source;
        private readonly IDbContextModelContext _context;
        private readonly IDotNetProjectFileInfoFactory _fileInfoFactory;
        public EntityMapperPackageContentProvider(
               IEntityMapperCodeGeneratorSource source,
               IDbContextModelContext context,
               IDotNetProjectFileInfoFactory fiFoctory)
        {
            _source = source;
            _context = context;
            _fileInfoFactory = fiFoctory;
        }


        public IEnumerable<IContent> Get()
        {
            var schema = _source.Schema;
            var tableName = _source.Name;
            var clrType = _source.Entity.ClrType;
            var className = $"{tableName}Extensions";

            var tableFullName = _source.Entity.GetTableFullName();
            var entityTable = _context.Entities.Single(e => e.TableFullName == tableFullName);

            var code = new ClassGenerator(new ClassGeneratorOptions()
            {
                Accessibility = AccessibilityLevels.Public,
                IsPartiale = true,
                Name = className,
                IsStatic = true,
                Namespace = clrType.Namespace,
                Contents = new List<IContentCodeSegment>()
                 {
                     new MethodMapper(_source.Entity,entityTable)
                 }

            });

            var str = code.Build();

            var dbProjOptions = _context.DotNetProjectTargetInfos;
            var rootPath = dbProjOptions.RootPath;
            var pattern = dbProjOptions.MapExtensionsPatternPath;
            var fileName = className;
            var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, schema, tableName, null, null);

            yield return new ContentFile(fi, new CommandTextSegment(str));
        }

        private class MethodMapper : IContentCodeSegment
        {
            private readonly IMutableEntityType _entity;
            private readonly IEntityTypeTable _entityTable;
            public MethodMapper(IMutableEntityType entity, IEntityTypeTable entityTable)
            {
                _entity = entity;
                _entityTable = entityTable;
                var usingsTypes = new List<Type>() { entity.ClrType, typeof(IDataRecord) };


                foreach (var type in usingsTypes)
                {
                    if (!string.IsNullOrEmpty(type.Namespace))
                        Usings.Add(type.Namespace);
                }

            }

            public List<string> Usings { get; } = new List<string>();

            public void Build(ICodeBuilder builder)
            {
                var type = _entity.ClrType;
                var name = type.Name;
                builder.AppendLine($"public static {name} Map(this {name} data, {nameof(IDataRecord)} dataRecord)");
                builder.AppendLine("{");
                using (builder.Indent())
                {
                    foreach (var column in _entityTable.AllColumns)
                        builder.AppendLine($"data.{column.PropertyName} = dataRecord.Get<{column.PropertyType}>(\"{column.ColumnName}\");");

                    builder.AppendLine($"return data;");
                }
                builder.AppendLine("}");
            }
        }
    }
}
