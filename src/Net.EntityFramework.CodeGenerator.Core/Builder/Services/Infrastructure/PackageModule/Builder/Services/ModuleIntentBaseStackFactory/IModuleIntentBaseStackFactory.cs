using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public interface IModuleIntentBaseStackFactory
    {
        IServiceCollection Create();
    }
}
