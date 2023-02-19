using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Data;

namespace DataBaseAccess
{

    public static class Tools
    {

        public static object ChangeType(object value, Type conversionType)
        {
            // Note: This if block was taken from Convert.ChangeType as is, and is needed here since we're
            // checking properties on conversionType below.
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            } // end if

            // If it's not a nullable type, just pass through the parameters to Convert.ChangeType

            if (conversionType.IsGenericType &&
              conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                // It's a nullable type, so instead of calling Convert.ChangeType directly which would throw a
                // InvalidCastException (per http://weblogs.asp.net/pjohnson/archive/2006/02/07/437631.aspx),
                // determine what the underlying type is
                // If it's null, it won't convert to the underlying type, but that's fine since nulls don't really
                // have a type--so just return null
                // Note: We only do this check if we're converting to a nullable type, since doing it outside
                // would diverge from Convert.ChangeType's behavior, which throws an InvalidCastException if
                // value is null and conversionType is a value type.
                if (value == null)
                {
                    return null;
                }

                // Ajout pour éviter des plantages avec une chaine vide
                if ((value as string) == string.Empty)
                {
                    return null;
                }

                // It's a nullable type, and not null, so that means it can be converted to its underlying type,
                // so overwrite the passed-in conversion type with this underlying type
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            // Now that we've guaranteed conversionType is something Convert.ChangeType can handle (i.e. not a
            // nullable type), pass the call on to Convert.ChangeType
            return System.Convert.ChangeType(value, conversionType);
        }


        private static readonly object BoxedEmpty = String.Empty;
        private static readonly string NllInt = typeof(int?).FullName;
        private static readonly string Nllbool = typeof(bool?).FullName;
        private static readonly string NllDateTime = typeof(DateTime?).FullName;
        private static readonly string Nlldouble = typeof(double?).FullName;
        private static readonly string Nllfloat = typeof(float?).FullName;
        private static readonly string Nlldecimal = typeof(decimal?).FullName;
        private static readonly string NllGuid = typeof(Guid?).FullName;
        private static readonly string NllTimeSpan = typeof(TimeSpan?).FullName;
        private static readonly string StringTypeName = typeof(string).FullName;
        private static readonly string BoolTypeName = typeof(bool).FullName;
        private static readonly string TrueValueSql = "1";
        private static readonly string FalseValueSql = "0";
        public static T Get<T>(this IDataRecord reader, string column)
        {

            var value = reader[column];

            if (value == DBNull.Value || value == null)
                return default(T);
            if (value == BoxedEmpty)
                return (T)BoxedEmpty;

            Type targetType = typeof(T);
            Type valueType = value.GetType();

            if (targetType.IsGenericType)
            {
                var key = targetType.FullName;
                Type targetGenericType = null;

                if (key == NllInt)
                    targetGenericType = typeof(int);
                else if (key == Nllbool)
                {
                    targetGenericType = typeof(bool);

                }
                else if (key == NllDateTime)
                    targetGenericType = typeof(DateTime);
                else if (key == Nlldouble)
                    targetGenericType = typeof(double);
                else if (key == Nllfloat)
                    targetGenericType = typeof(float);
                else if (key == Nlldecimal)
                    targetGenericType = typeof(decimal);
                else if (key == NllGuid)
                    targetGenericType = typeof(Guid);
                else if (key == NllTimeSpan)
                    targetGenericType = typeof(TimeSpan);
                else
                    targetGenericType = targetType.GenericTypeArguments[0];//couteux..

                if (targetGenericType == valueType)
                    return (T)value;

                if (valueType.FullName == StringTypeName && key == Nllbool)
                {
                    var str = value.ToString();
                    if (str == TrueValueSql)
                        return (T)(object)true;
                    if (str == FalseValueSql)
                        return (T)(object)false;
                }

                return (T)System.Convert.ChangeType(value, targetGenericType);
            }


            if (targetType == valueType)
                return (T)value;
            if (valueType.FullName == StringTypeName && targetType.FullName == BoolTypeName)
            {
                var str = value.ToString();
                if (str == TrueValueSql)
                    return (T)(object)true;
                if (str == FalseValueSql)
                    return (T)(object)false;
            }
            return (T)System.Convert.ChangeType(value, targetType);
        }



        public static IEnumerable<string> GetSplit(this SqlDataReader reader, string column, char separator, StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries)
        {
            var str = reader.Get<string>(column);
            if (!string.IsNullOrEmpty(str))
            {
                foreach (var item in str.Split(separator, stringSplitOptions))
                {
                    yield return item;  
                }
            }
        }
    }
}
