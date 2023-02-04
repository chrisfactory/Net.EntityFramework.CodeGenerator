using Microsoft.Extensions.DependencyInjection;

namespace SqlG
{
    internal static class ICreateFileActionBuilderExtensions
    {
        public static ICreateFileActionBuilder UseCommandText(this ICreateFileActionBuilder builder, string commandText)
        {
           return  builder.UseFileAction(new CommandTextSegment(commandText)); 
        }
        public static ICreateFileActionBuilder UseFileAction<TFileAction>(this ICreateFileActionBuilder builder, TFileAction fileAction)
            where TFileAction : IContentFileRootSegment
        {
            builder.Services.AddSingleton<IContentFileRootSegment>(fileAction);
            return builder;
        }
        public static ICreateFileActionBuilder UseTargetFiles(this ICreateFileActionBuilder builder, params FileInfo[] targets)
        {
            builder.Services.AddSingleton<IFileInfosProvider>(new TargetFiles(targets));
            return builder;
        }
    }
}
