using System.Globalization;

namespace ExternalEntities.Infraestructure
{
    public static class DecimalExtensions
    {
        public static decimal TryParseDecimal(this string value)
        {
            decimal? parsedValue = null;
            decimal parsedValueHelper;

            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out parsedValueHelper))
                parsedValue = parsedValueHelper;
            
            return parsedValue ?? 0;
        }
        public static decimal TryParseDecimal(this double value)
        { 
            decimal? parsedValue = null;
            decimal parsedValueHelper;

            if (decimal.TryParse($"{value}", out parsedValueHelper))
                parsedValue = parsedValueHelper;

            return parsedValue ?? 0;
        }
        public static decimal TryParseDecimal(this int value)
        {
            decimal? parsedValue = null;
            decimal parsedValueHelper;

            if (decimal.TryParse($"{value}", out parsedValueHelper))
                parsedValue = parsedValueHelper;

            return parsedValue ?? 0;
        }

        public static int TryParseInt(this string value)
        {
            int? parsedValue = null;
            int parsedValueHelper;

            if (int.TryParse(value, out parsedValueHelper))
                parsedValue = parsedValueHelper;

            return parsedValue ?? 0;
        }
    }
}
