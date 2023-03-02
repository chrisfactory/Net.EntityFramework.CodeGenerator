namespace EntityFramework.CodeGenerator.Core
{
    internal static class ToCSharpTypes
    {
        internal static string ToCSharpString(this Type type)
        {
            if (type == null)
                return string.Empty;

            if (!type.IsGenericType)
            {
                if (type == typeof(System.Int32))
                    return "int";
                if (type == typeof(System.String))
                    return "string";
                if (type == typeof(System.Decimal))
                    return "decimal";
                if (type == typeof(System.Boolean))
                    return "bool";
                if (type == typeof(System.Byte))
                    return "byte";
                if (type == typeof(System.SByte))
                    return "sbyte";
                if (type == typeof(System.Char))
                    return "char";
                if (type == typeof(System.Double))
                    return "double";
                if (type == typeof(System.Single))
                    return "float";
                if (type == typeof(System.UInt32))
                    return "uint";
                if (type == typeof(System.IntPtr))
                    return "nint";
                if (type == typeof(System.UIntPtr))
                    return "nuint";
                if (type == typeof(System.Int64))
                    return "long";
                if (type == typeof(System.UInt64))
                    return "ulong";
                if (type == typeof(System.Int16))
                    return "short";
                if (type == typeof(System.UInt16))
                    return "ushort";
                if (type == typeof(System.Object))
                    return "object";

                return type.Name;
            }

            if (type == typeof(Nullable<System.Int32>))
                return "int?";
            if (type == typeof(Nullable<System.Decimal>))
                return "decimal?";
            if (type == typeof(Nullable<System.Boolean>))
                return "bool?";
            if (type == typeof(Nullable<System.Byte>))
                return "byte?";
            if (type == typeof(Nullable<System.SByte>))
                return "sbyte?";
            if (type == typeof(Nullable<System.Char>))
                return "char?";
            if (type == typeof(Nullable<System.Double>))
                return "double?";
            if (type == typeof(Nullable<System.Single>))
                return "float?";
            if (type == typeof(Nullable<System.UInt32>))
                return "uint?";
            if (type == typeof(Nullable<System.IntPtr>))
                return "nint?";
            if (type == typeof(Nullable<System.UIntPtr>))
                return "nuint?";
            if (type == typeof(Nullable<System.Int64>))
                return "long?";
            if (type == typeof(Nullable<System.UInt64>))
                return "ulong?";
            if (type == typeof(Nullable<System.Int16>))
                return "short?";
            if (type == typeof(Nullable<System.UInt16>))
                return "ushort?";

            return type.Name;
        }




    }
}
