using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Net.EntityFramework.CodeGenerator.Core;

namespace Net.EntityFramework.CodeGenerator.SqlServer
{
    internal class EFDbServicePackageContentProvider : IIntentContentProvider
    {
        private readonly IDbContextModelContext _context;
        private readonly IMutableEntityType _mutableEntity;
        private readonly IDotNetProjectFileInfoFactory _fileInfoFactory;
        private readonly IPackageToken _token;
        private readonly IPackageLink _link;
        public EFDbServicePackageContentProvider(
               IDbContextModelContext context,
               IMutableEntityType mutableEntity,
               IDotNetProjectFileInfoFactory fiFoctory,
               IPackageToken token,
               IPackageLink link)
        {
            _context = context;
            _mutableEntity = mutableEntity;
            _fileInfoFactory = fiFoctory;
            _token = token;
            _link = link;
        }

        public IEnumerable<IContent> Get()
        {
            if (!_context.IsSelfDbContext)
            {
                var dbContextType = _context.DbContextType;

                var schema = _mutableEntity.GetSchema();
                var tableName = _mutableEntity.ClrType.Name;
                var className = $"{dbContextType.Name}Extensions";


                var code = new ClassGenerator(new ClassGeneratorOptions()
                {
                    Accessibility = AccessibilityLevels.Public,
                    IsPartiale = true,
                    Name = className,
                    IsStatic = true,
                    Namespace = dbContextType.Namespace,
                    Contents = new List<IDotNetContentCodeSegment>(GetSegments(_token, _link))
                });
                var str = code.Build();

                var dbProjOptions = _context.DotNetProjectTargetInfos;
                var rootPath = dbProjOptions.RootPath;
                var pattern = dbProjOptions.DbContextExtensionsPatternPath;
                var fileName = $"{className}.{tableName}";
                var fi = _fileInfoFactory.CreateFileInfo(rootPath, fileName, pattern, schema, tableName, null, null);

                yield return new ContentFile(fi, new CommandTextSegment(str));


            }
        }
        private IReadOnlyList<IDotNetContentCodeSegment> GetSegments(IPackageToken token, IPackageLink link)
        {
            var segments = new List<IDotNetContentCodeSegment>();
            var keys = token.CorrelateTokens.ToHashSet();
            foreach (var package in link.Packages)
            {
                if (keys.Contains(package.Token))
                {
                    foreach (var intent in package.Intents)
                    {
                        if (intent.Target is IEfDbContextExtensionBuilderTarget serviceBuilderTarget)
                        {
                            foreach (var content in intent.Contents)
                            {
                                if (content is IDotNetContentCodeSegment storedProcedure)
                                {
                                    segments.Add(storedProcedure);
                                }
                            }
                        }
                    }
                }
            }

            return segments;
        }

    }
}
