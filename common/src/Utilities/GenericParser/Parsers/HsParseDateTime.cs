using Blogposts.Common.Utilities.GenericParser.Core;
using System;

namespace Blogposts.Common.Utilities.GenericParser.Parsers
{
    public class HsParseDateTime : HsParseCore<DateTime>
    {
        public override DateTime Decrease(DateTime value, DateTime step, DateTime min)
        {
            throw new NotImplementedException();
        }

        public override string FormatValueAsString(DateTime value, string format)
        {
            return value.ToString(format);
        }

        public override DateTime FromBool(bool value)
        {
            throw new NotImplementedException();
        }

        public override DateTime FromBoolNull(bool? value, bool indeterminate)
        {
            throw new NotImplementedException();
        }

        public override DateTime FromDecimal(decimal value)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetMaximum()
        {
            throw new NotImplementedException();
        }

        public override DateTime GetMinimum()
        {
            throw new NotImplementedException();
        }

        public override DateTime GetStep()
        {
            throw new NotImplementedException();
        }

        public override DateTime Increase(DateTime value, DateTime step, DateTime max)
        {
            throw new NotImplementedException();
        }

        public override DateTime ParseFromString(string value, string format)
        {
            return DateTime.Parse(value,new System.Globalization.CultureInfo("en-GB"));
        }

        public override DateTime Round(DateTime value, int decPlace)
        {
            throw new NotImplementedException();
        }

        public override bool ToBool(DateTime value)
        {
            throw new NotImplementedException();
        }
    }
}