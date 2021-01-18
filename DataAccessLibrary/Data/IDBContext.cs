using System;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IDBContext
    {
        Task InsertRecordAsync<T>(string table, T record);

        Task<PagedCollection<T>> GetRecordsAsync<T>(string table, int page, int pageSize);

        Task<bool> UpdateAsync<T>(string table, Guid id, T record);

        Task<bool> DeleteRecordAsync<T>(string table, Guid id);
    }
}
