using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EFDbServicePackageContentProvider : IIntentContentProvider
    {

        private readonly IEntityMapperCodeGeneratorSource _source;
        private readonly IDbContextModelContext _context;
        private readonly IDotNetProjectFileInfoFactory _fileInfoFactory;
        public EFDbServicePackageContentProvider(
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
                //Contents = new List<IContentCodeSegment>()
                // {
                //     new MethodMapper(_source.Entity,entityTable)
                // }

            });
            var str = code.Build();

            var dbProjOptions = _context.DotNetProjectTargetInfos;
            var rootPath = dbProjOptions.RootPath;
            var pattern = dbProjOptions.MapExtensionsPatternPath;
            var fileName = className;
            var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, schema, tableName, null, null);

            yield return new ContentFile(fi, new CommandTextSegment(str));



            yield return new CommandTextSegment("EFDbService");
        }
    }

    //public static Food2? SelectCustomFood2ByFoodId(this SampleDbContext db, int id)
    //       => db.Set<Food2>().FromSql($"EXECUTE [dbo].[SelectCustomFood2ByFoodId] @FoodId={id}")
    //       .AsEnumerable()
    //       .SingleOrDefault();
    //public static async Task<Food2?> SelectCustomFood2ByFoodIdAsync(this SampleDbContext db, int id)
    //=> (await db.Set<Food2>().FromSql($"EXECUTE [dbo].[SelectCustomFood2ByFoodId] @FoodId={id}").ToListAsync()).SingleOrDefault();
}
