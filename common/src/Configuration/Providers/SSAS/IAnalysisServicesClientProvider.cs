using System;
using System.Collections.Generic;
using System.Data;

namespace Assets.Configuration
{
    public interface IAnalysisServicesClientProvider
    {
        List<KeyValuePair<T, long>> ExecuteAndFill<T>(string commandText, Func<IDataReader, KeyValuePair<T, long>> func);
    }
}