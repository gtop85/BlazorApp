using AutoMapper;
using DataAccessLibrary;
using System;
using System.Collections.Generic;
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
            customer.Id = customerData.Id.ToString();
            
            return customer;
        }

        public async Task<List<CustomerViewModel>> GetCustomersAsync(int limit, int offset)
        {
            var data = await _dB.GetCustomersAsync<CustomerDataModel>();
            List<CustomerViewModel> result = _mapper.Map<List<CustomerDataModel>, List<CustomerViewModel>>(data);

            return result;
        }

        public async Task<CustomerViewModel> UpdateCustomerAsync(string id, CustomerViewModel customer)
        {
            if (!Guid.TryParse(id, out Guid guid)) return null;
            var customerData = _mapper.Map<CustomerViewModel, CustomerDataModel>(customer);
            var result = await _dB.UpdateCustomerAsync(guid, customerData);
            customer.Id = id;

            if (!result) return null;
            return customer;
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid guid)) return false;

            var result = await _dB.DeleteCustomerAsync<CustomerDataModel>(guid);

            return result;
        }
    }
}
