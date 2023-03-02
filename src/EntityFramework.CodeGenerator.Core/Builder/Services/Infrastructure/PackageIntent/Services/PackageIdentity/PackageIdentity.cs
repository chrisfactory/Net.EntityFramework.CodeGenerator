﻿using EntityFramework.CodeGenerator.Core;
using System.Diagnostics;

namespace EntityFramework.CodeGenerator
{
    [DebuggerDisplay("{Name}")]
    internal class PackageIdentity : IPackageIdentity
    {
        public PackageIdentity(IPackageContentSource src)
        {
            Name = $"{src.Name} { src.Scope.GetDisplayName()}";
        }
        public string Name { get; }
    }
}