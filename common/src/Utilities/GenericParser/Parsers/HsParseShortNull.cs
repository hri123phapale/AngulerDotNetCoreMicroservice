using Blogposts.Common.Utilities.GenericParser.Core;

namespace Blogposts.Common.Utilities.GenericParser.Parsers
{
    /// <summary>
    /// Generic short null parser
    /// </summary>
    public class HsParseShortNull : HsParseCore<short?>
    {
        public override short? Decrease(short? value, short? step, short? min)
        {
            throw new System.NotImplementedException();
        }

        public override string FormatValueAsString(short? value, string format)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public override short? FromBool(bool value)
        {
            throw new System.NotImplementedException();
        }

        public override short? FromBoolNull(bool? value, bool indeterminate)
        {
            throw new System.NotImplementedException();
        }

        public override short? FromDecimal(decimal value)
        {
            throw new System.NotImplementedException();
        }

        public override short? GetMaximum()
        {
            throw new System.NotImplementedException();
        }

        public override short? GetMinimum()
        {
            throw new System.NotImplementedException();
        }

        public override short? GetStep()
        {
            throw new System.NotImplementedException();
        }

        public override short? Increase(short? value, short? step, short? max)
        {
            throw new System.NotImplementedException();
        }

        public override short? ParseFromString(string value, string format)
        {
            short? result = null;
            if (!string.IsNullOrEmpty(value))
            {
                result = short.Parse(value);
            }
            return result;
        }

        public override short? Round(short? value, int decPlace)
        {
            throw new System.NotImplementedException();
        }

        public override bool ToBool(short? value)
        {
            throw new System.NotImplementedException();
        }
    }
}