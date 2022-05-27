using System;


namespace appDostava.Extensions
{
    public static class StringExtensions
    {


        public static string CapitalizeFirstLetter(this string value) =>
            value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                "" => throw new ArgumentNullException($"{nameof(value)} can't be empty"),
                _ => string.Concat(value[0].ToString().ToUpper(), value.AsSpan(1))
            };


    }
}
