using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Net.EntityFramework.CodeGenerator.Core.Tools
{
    internal static class IReadOnlyAnnotatableExtensions
    {
        public static bool TryGetAnnotation<T>(this IReadOnlyAnnotatable annotable, string key, out T? result)
        {
            result = default(T);
            var annotation = annotable.FindAnnotation(key);

            if (annotation == null)
                return false;
            if (annotation.Value == null)
                return false;

            if(annotation.Value is T resultTemp)
            {
                result = resultTemp;
                return true;
            }

            return false; 
        }
    }
}
