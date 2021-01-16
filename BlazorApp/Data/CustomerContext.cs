using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class CustomerContext : ICustomerContext
    {
        private readonly string CustomersTable = "Customers";
        private readonly IDBContext _db;

        public CustomerContext(IDBContext dB)
        {
            _db = dB;
        }

        public async Task InsertCustomerAsync(CustomerDataModel customerData)
        {
            await _db.InsertRecordAsync(CustomersTable, customerData);
        }

        public async Task<List<CustomerDataModel>> GetCustomersAsync<T>()
        {
            var result = await _db.GetRecordsAsync<CustomerDataModel>(CustomersTable);

            return result;
        }

        public async Task<bool> UpdateCustomerAsync(Guid id, CustomerDataModel customerData)
        {
            var result = await _db.UpdateAsync(CustomersTable, id, customerData);

            return result;
        }

        public async Task<bool> DeleteCustomerAsync<T>(Guid id)
        {
            var result = await _db.DeleteRecordAsync<CustomerDataModel>(CustomersTable, id);

            return result;
        }
    }
}
