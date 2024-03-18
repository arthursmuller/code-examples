using System.Globalization;

namespace ExternalEntities.Domain
{
    public static class NumberHelper
    {
        public static decimal FromDecimalString(string value)
        {
            if (value.Contains(","))
                return decimal.Parse(value, new NumberFormatInfo { NumberDecimalSeparator = "," });

            return decimal.Parse(value, new NumberFormatInfo { NumberDecimalSeparator = "." });
        }

        public static string FromDecimal(decimal? value)
        {
            return value?.ToString(CultureInfo.InvariantCulture);
        }

        public static int? TryParseInt(string value)
        {
            int? parsedValue = null;
            int parsedValueHelper;

            if (int.TryParse(value, out parsedValueHelper))
                parsedValue = parsedValueHelper;

            return parsedValue;
        }

        public static double FromDoubleString(string input)
        {
            if (input.Contains(','))
            {
                input = input.Replace(',', '.');
            }

            return double.Parse(input, CultureInfo.InvariantCulture);
        }
    }
}
