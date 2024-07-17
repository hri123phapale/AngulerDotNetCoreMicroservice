using Blogposts.Common.Utilities.GenericParser.Core;
using System;

namespace Blogposts.Common.Utilities.GenericParser.Parsers
{
    public class HsParseGuidNull : HsParseCore<Guid?>
    {
        public override Guid? Decrease(Guid? value, Guid? step, Guid? min)
        {
            throw new NotImplementedException();
        }

        public override string FormatValueAsString(Guid? value, string format)
        {
            string stringValue = string.Empty;
            if (value != null)
            {
                stringValue = value.ToString();
            }
            return stringValue;
        }

        public override Guid? FromBool(bool value)
        {
            throw new NotImplementedException();
        }

        public override Guid? FromBoolNull(bool? value, bool indeterminate)
        {
            throw new NotImplementedException();
        }

        public override Guid? FromDecimal(decimal value)
        {
            throw new NotImplementedException();
        }

        public override Guid? GetMaximum()
        {
            throw new NotImplementedException();
        }

        public override Guid? GetMinimum()
        {
            throw new NotImplementedException();
        }

        public override Guid? GetStep()
        {
            throw new NotImplementedException();
        }

        public override Guid? Increase(Guid? value, Guid? step, Guid? max)
        {
            throw new NotImplementedException();
        }

        public override Guid? ParseFromString(string value, string format)
        {
            Guid? guidValue = null;
            if (!string.IsNullOrEmpty(value))
            {
                guidValue = new Guid(value);
            }
            return guidValue;
        }

        public override Guid? Round(Guid? value, int decPlace)
        {
            throw new NotImplementedException();
        }

        public override bool ToBool(Guid? value)
        {
            throw new NotImplementedException();
        }
    }
}