using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp
{
    public interface ICustomerContext
    {
        Task<bool> DeleteCustomerAsync<T>(Guid id);
        Task<List<CustomerDataModel>> GetCustomersAsync<T>();
        Task InsertCustomerAsync(CustomerDataModel customerData);
        Task<bool> UpdateCustomerAsync(Guid id, CustomerDataModel customerData);
    }
}