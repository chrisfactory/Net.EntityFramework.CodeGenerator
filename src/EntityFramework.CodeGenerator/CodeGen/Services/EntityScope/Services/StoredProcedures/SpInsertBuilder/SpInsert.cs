using System.Text;
using EntityFramework.CodeGenerator.Core;
namespace EntityFramework.CodeGenerator
{
    internal class SpInsert : IContentFileSegment
    {
        private readonly IEntityTypeTable _entity;
        private readonly string _spName;
        public SpInsert(IEntityTypeTable entity, string spName)
        {
            _entity = entity;
            _spName = spName;
        }

        public void Build(StringBuilder builder)
        {
            var sc = _entity.Table.Schema;
            var tempName = "@OutputTable";

            builder.BuildCreateProcedure(sc, _spName, _entity.UpdatableColumns);
            builder.AppendLine();
            builder.AppendLine();
            builder.BuildTalbleTemp(_entity.AllColumns, tempName);
            builder.AppendLine();
            builder.AppendLine();
            builder.BuildInsertInto(sc, _entity.Table.Name, _entity.UpdatableColumns);
            builder.BuildInsertOutput(_entity.UpdatableColumns, tempName);
            builder.BuildInsertValues(_entity.UpdatableColumns, 1);
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine("SELECT * FROM @OutputTable");
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine("END");

        }
    }
}
