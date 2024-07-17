using Blogposts.Common.Utilities.GenericParser.Core;

namespace Blogposts.Common.Utilities.GenericParser.Parsers
{
    /// <summary>
    /// Generic string parser
    /// </summary>
    public class HsParseString : HsParseCore<string>
    {
        /// <inheritdoc/>
        public override string Decrease(string value, string step, string min)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override string FormatValueAsString(string value, string format)
        {
            return value;
        }

        /// <inheritdoc/>
        public override string FromBool(bool value)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override string FromBoolNull(bool? value, bool indeterminate)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override string FromDecimal(decimal value)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override string GetMaximum()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override string GetMinimum()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override string GetStep()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override string Increase(string value, string step, string max)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override string ParseFromString(string value, string format)
        {
            return value;
        }

        /// <inheritdoc/>
        public override string Round(string value, int decPlace)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool ToBool(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}