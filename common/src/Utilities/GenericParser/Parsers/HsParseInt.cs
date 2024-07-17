using Blogposts.Common.Utilities.GenericParser.Core;

namespace Blogposts.Common.Utilities.GenericParser.Parsers
{
    /// <summary>
    /// Generic int null parser
    /// </summary>
    public class HsParseInt : HsParseCore<int>
    {
        public override int Decrease(int value, int step, int min)
        {
            throw new System.NotImplementedException();
        }

        public override string FormatValueAsString(int value, string format)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public override int FromBool(bool value)
        {
            throw new System.NotImplementedException();
        }

        public override int FromBoolNull(bool? value, bool indeterminate)
        {
            throw new System.NotImplementedException();
        }

        public override int FromDecimal(decimal value)
        {
            throw new System.NotImplementedException();
        }

        public override int GetMaximum()
        {
            throw new System.NotImplementedException();
        }

        public override int GetMinimum()
        {
            throw new System.NotImplementedException();
        }

        public override int GetStep()
        {
            throw new System.NotImplementedException();
        }

        public override int Increase(int value, int step, int max)
        {
            throw new System.NotImplementedException();
        }

        public override int ParseFromString(string value, string format)
        {
            int result = -1;
            if (!string.IsNullOrEmpty(value))
            {
                result = int.Parse(value);
            }
            return result;
        }

        public override int Round(int value, int decPlace)
        {
            throw new System.NotImplementedException();
        }

        public override bool ToBool(int value)
        {
            throw new System.NotImplementedException();
        }
    }
}