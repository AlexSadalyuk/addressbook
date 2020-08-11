using System;
using System.Collections.Generic;
using System.Data;

namespace DbReader.Interfaces
{
    public interface IDbReader : IDisposable
    {
        IEnumerable<T> Exec<T>(string query, 
                               Dictionary<string, string> parameters = null, 
                               CommandType type = CommandType.StoredProcedure) where T : class, new();
    }
}
