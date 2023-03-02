﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.CodeGenerator
{
    public static partial class ISqlGenEntityBuilderExtensions
    {
        internal const string ModelGenerateAnnotationKey = nameof(ModelGenerateAnnotationKey);
        public static ModelBuilder Generate(this ModelBuilder modelBuilder, Action<ISqlGenModelBuilder> builder)
        {
            var b = new SqlGenModelBuilder();
            builder?.Invoke(b);
            modelBuilder.HasAnnotation(ModelGenerateAnnotationKey, b.Build());
            return modelBuilder;
        }

        public static ISqlGenModelBuilder AddGenAction<TAction>(this ISqlGenModelBuilder builder, TAction action)
            where TAction : IActionProvider
        {
            builder.Services.AddSingleton<IActionProvider>(action);
            return builder;
        }
    }



}
