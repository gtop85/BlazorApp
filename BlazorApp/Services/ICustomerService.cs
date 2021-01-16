using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp
{
    public interface ICustomerService
    {
        Task<CustomerViewModel> CreateCustomerAsync(CustomerViewModel customer);
        Task<List<CustomerViewModel>> GetCustomersAsync(int limit, int offset);
        Task<CustomerViewModel> UpdateCustomerAsync(string id, CustomerViewModel customer);
        Task<bool> DeleteCustomerAsync(string id);
    }
}
