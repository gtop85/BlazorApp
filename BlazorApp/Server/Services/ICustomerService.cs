using System;
using System.Threading.Tasks;

namespace BlazorApp
{
    public interface ICustomerService
    {
        Task<CustomerViewModel> CreateCustomerAsync(CustomerViewModel customer);
        Task<CustomerCollection> FetchCustomersAsync(PaginationDTO pagination);
        Task<CustomerViewModel> UpdateCustomerAsync(Guid id, CustomerViewModel customer);
        Task<bool> DeleteCustomerAsync(Guid id);
    }
}
