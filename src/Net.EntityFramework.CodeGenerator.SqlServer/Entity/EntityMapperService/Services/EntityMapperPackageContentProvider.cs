using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EntityMapperPackageContentProvider : IIntentContentProvider
    {
        private readonly IEntityMapperCodeGeneratorSource _source;
        public EntityMapperPackageContentProvider(IEntityMapperCodeGeneratorSource source)
        {
            _source = source;
        }

        public IEnumerable<IContent> Get()
        {
            //var spSchema = _source.Schema;
            //var spName = _source.Name;
            //var targetTableFullName = _source.TableFullName;
            //var projectionColumns = _source.ProjectionColumns;
            //var primaryKeys = _source.PrimaryKeys;

            //var spOptions = new StoredProcedureOptions(spSchema, spName);
            //var sp = new StoredProcedureGenerator(spOptions, primaryKeys, new SelectGenerator(targetTableFullName, false, projectionColumns, primaryKeys));
            //var spCode = sp.Build();

            var code = new ClassGenerator(new ClassGeneratorOptions()
            {
                Accessibility = AccessibilityLevels.Public,
                IsPartiale = true,
                Name = "Name",
                IsStatic = true,
                Namespace = "System",

            });

            var str = code.Build();

            yield return new CommandTextSegment("prouuuut");
        }
    }


}
