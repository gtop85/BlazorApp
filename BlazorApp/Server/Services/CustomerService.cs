using AutoMapper;
using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerContext _dB;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerContext db, IMapper mapper)
        {
            _dB = db;
            _mapper = mapper;
        }

        public async Task<CustomerViewModel> CreateCustomerAsync(CustomerViewModel customer)
        {
            CustomerDataModel customerData = _mapper.Map<CustomerDataModel>(customer);
            await _dB.InsertCustomerAsync(customerData);
            //var createdCustomer = _mapper.Map<CustomerViewModel>(customerData);
            customer.Id = customerData.Id;

            return customer;
        }

        public async Task<CustomerCollection> FetchCustomersAsync(PaginationDTO pagination)
        {
            var data = await _dB.GetCustomersAsync(pagination);
            CustomerCollection result = _mapper.Map<PagedCollection<CustomerDataModel>, CustomerCollection>(data);

            return result;
        }

        public async Task<CustomerViewModel> UpdateCustomerAsync(Guid id, CustomerViewModel customer)
        {
            if (customer == null || customer.Id == null || customer.Id == Guid.Empty || id == Guid.Empty) return null;
            if (customer.Id != id) return null;
            var customerData = _mapper.Map<CustomerViewModel, CustomerDataModel>(customer);
            var result = await _dB.UpdateCustomerAsync(id, customerData);
            customer.Id = id;

            if (!result) return null;
            return customer;
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            if (id == Guid.Empty) return false;
            var result = await _dB.DeleteCustomerAsync(id);

            return result;
        }
    }
}
