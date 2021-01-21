using BlazorApp;
using System;
using Xunit;

namespace UnitTests
{
    public class CrudTests
    {
        [Fact]
        public async void GetCustomersSuccess()
        {
            ICustomerService customerService = Extensions.MockCustomerServiceForGet();

            var result = await customerService.FetchCustomersAsync(new PaginationDTO());

            Assert.NotNull(result);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(25, result.TotalCount);
            Assert.Equal(3, result.TotalPages);
            Assert.Equal(2, result.Items.Count);
        }        

        [Fact]
        public async void GetCustomersFailure()
        {
            ICustomerService customerService = Extensions.MockCustomerServiceForGet();

            var result = await customerService.FetchCustomersAsync(new PaginationDTO());

            Assert.NotNull(result);
            Assert.NotEqual(5, result.CurrentPage);
            Assert.NotEqual(100, result.TotalCount);
            Assert.NotEqual(10, result.TotalPages);
            Assert.NotEqual(20, result.Items.Count);
        }


        [Fact]
        public async void InsertCustomerSuccess()
        {
            CustomerViewModel newCustomer = Extensions.GenerateCustomer(false);
            ICustomerService customerService = Extensions.MockCustomerServiceForPost();

            var result = await customerService.CreateCustomerAsync(newCustomer);

            Assert.NotNull(result);
            Assert.Equal(newCustomer.Address, result.Address);
            Assert.Equal(newCustomer.City, result.City);
            Assert.Equal(newCustomer.CompanyName, result.CompanyName);
            Assert.Equal(newCustomer.ContactName, result.ContactName);
            Assert.Equal(newCustomer.Country, result.Country);
            Assert.Equal(newCustomer.Phone, result.Phone);
            Assert.Equal(newCustomer.PostalCode, result.PostalCode);
            Assert.Equal(newCustomer.Region, result.Region);
        }

        [Fact]
        public async void InsertCustomerFailure()
        {
            ICustomerService customerService = Extensions.MockCustomerServiceForPost();

            CustomerViewModel newCustomer = Extensions.GenerateCustomer(false);
            var result = await customerService.CreateCustomerAsync(newCustomer);

            Assert.NotNull(result);
            Assert.NotEqual("Address", result.Address);
            Assert.NotEqual("City", result.City);
            Assert.NotEqual("CompanyName", result.CompanyName);
            Assert.NotEqual("ContactName", result.ContactName);
            Assert.NotEqual("Country", result.Country);
            Assert.NotEqual("Phone", result.Phone);
            Assert.NotEqual("PostalCode", result.PostalCode);
            Assert.NotEqual("Region", result.Region);
        }


        [Fact]
        public async void UpdateCustomerSuccess()
        {
            CustomerViewModel customer = Extensions.GenerateCustomer();
            ICustomerService customerService = Extensions.MockCustomerServiceForUpdate();

            var result = await customerService.UpdateCustomerAsync(customer.Id.Value, customer);

            Assert.NotNull(result);
            Assert.Equal(customer.Address, result.Address);
            Assert.Equal(customer.City, result.City);
            Assert.Equal(customer.CompanyName, result.CompanyName);
            Assert.Equal(customer.ContactName, result.ContactName);
            Assert.Equal(customer.Country, result.Country);
            Assert.Equal(customer.Phone, result.Phone);
            Assert.Equal(customer.PostalCode, result.PostalCode);
            Assert.Equal(customer.Region, result.Region);
        }


        [Theory]
        [ClassData(typeof(CustomerDataGenerator))]
        public async void UpdateCustomerFailure(Guid id, CustomerViewModel customer)
        {
            ICustomerService customerService = Extensions.MockCustomerServiceForUpdate();

            var result = await customerService.UpdateCustomerAsync(id, customer);

            Assert.Null(result);
        }


        [Fact]
        public async void DeleteCustomerSuccess()
        {
            ICustomerService customerService = Extensions.MockCustomerServiceForDelete();

            var result = await customerService.DeleteCustomerAsync(Guid.NewGuid());

            Assert.True(result);
        }

        [Fact]
        public async void DeleteCustomerFailure()
        {
            ICustomerService customerService = Extensions.MockCustomerServiceForDelete();

            var result = await customerService.DeleteCustomerAsync(Guid.Empty);

            Assert.False(result);
        }
    }
}
