using Microsoft.EntityFrameworkCore;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EFDbServicePackageContentProvider : IIntentContentProvider
    {
        private readonly IDbContextModelContext _context;
        private readonly IDotNetProjectFileInfoFactory _fileInfoFactory;
        public EFDbServicePackageContentProvider(
               IDbContextModelContext context,
               IDotNetProjectFileInfoFactory fiFoctory)
        {
            _context = context;
            _fileInfoFactory = fiFoctory;
        }

        public IEnumerable<IContent> Get()
        {
            if (!_context.IsSelfDbContext)
            {
                var dbContextType = _context.DbContextType;

                var schema = "_source.Schema";
                var tableName = "_source.Name";
                //var clrType = _source.Entity.ClrType;
                var className = $"{dbContextType.Name}Extensions";

                //var tableFullName = _source.Entity.GetTableFullName();
                //  var entityTable = _context.Entities.Single(e => e.TableFullName == tableFullName);


                var code = new ClassGenerator(new ClassGeneratorOptions()
                {
                    Accessibility = AccessibilityLevels.Public,
                    IsPartiale = true,
                    Name = className,
                    IsStatic = true,
                    Namespace = dbContextType.Namespace,
                  //  Contents = new List<IDotNetContentCodeSegment>(_source.Segments)
                });
                var str = code.Build();

                var dbProjOptions = _context.DotNetProjectTargetInfos;
                var rootPath = dbProjOptions.RootPath;
                var pattern = dbProjOptions.MapExtensionsPatternPath;
                var fileName = $"{className}.{tableName}";
                var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, schema, tableName, null, null);

                yield return new ContentFile(fi, new CommandTextSegment(str));


            }
        }

    }
}
