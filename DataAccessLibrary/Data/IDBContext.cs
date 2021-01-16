using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IDBContext
    {
        Task InsertRecordAsync<T>(string table, T record);

        Task<List<T>> GetRecordsAsync<T>(string table);

        Task<bool> UpdateAsync<T>(string table, Guid id, T record);

        Task<bool> DeleteRecordAsync<T>(string table, Guid id);
    }
}
