﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Net.EntityFramework.CodeGenerator.Core
{
    public static partial class ICodeGeneratorBuilderExtensions
    {
        public static ICodeGeneratorBuilder UseSelfModeDbContext(this ICodeGeneratorBuilder builder, Action<ModelBuilder> modelBuilder, Action<DbContextOptionsBuilder>? optionsAction = null)
        {
            builder.Services.AddSingleton(modelBuilder);
            return builder.UseDbContext<SelfDbContext>(optionsAction);
        }
        public static ICodeGeneratorBuilder UseDbContext<TDbContext>(this ICodeGeneratorBuilder builder, Action<DbContextOptionsBuilder>? optionsAction = null)
            where TDbContext : DbContext
        {
            builder.Services.AddDbContext<TDbContext>(optionsAction);
            builder.Services.AddSingleton(pb => (DbContext)pb.GetRequiredService<TDbContext>());
            return builder;
        }
    }
}
