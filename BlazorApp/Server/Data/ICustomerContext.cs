using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp
{
    public interface ICustomerContext
    {
        Task<PagedCollection<CustomerDataModel>> GetCustomersAsync(PaginationDTO pagination);
        Task InsertCustomerAsync(CustomerDataModel customerData);
        Task<bool> UpdateCustomerAsync(Guid id, CustomerDataModel customerData);
        Task<bool> DeleteCustomerAsync(Guid id);
    }
}