using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlG
{
    public interface IEntityTypeTable
    {
        public IModel Model { get; }
        public IEntityType EntityType { get; }
        public ITable Table { get; }
    }
}
