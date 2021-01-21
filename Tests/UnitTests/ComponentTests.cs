using AutoMapper;
using BlazorApp;
using BlazorApp.Pages;
using BlazorApp.Shared;
using Bunit;
using DataAccessLibrary;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ComponentTests : TestContext
    {
        [Fact]
        public void TestCustomerFormHeader()
        {
            // Arrange
            var customerService = Extensions.MockCustomerServiceForGet(false);
            Services.AddSingleton(customerService);
            // Act
            var cut = RenderComponent<CustomerForm>().Find("h3");

            // Assert
            var initialExpectedHtml =
                        @"<h3>Enter customer's details</h3>";
            cut.MarkupMatches(initialExpectedHtml);
        }

        [Fact]
        public void TestFetchData_NoCustomerData()
        {
            // Arrange
            var customerData = new List<CustomerViewModel>();
            var customerService = Extensions.MockCustomerServiceForGet(false);
            Services.AddSingleton(customerService);

            // Act
            var cut = RenderComponent<CustomerTable>();

            // Assert
            var expectedDataTable = RenderComponent<CustomerTable>((nameof(CustomerTable.Customers), customerData));
            cut.MarkupMatches(expectedDataTable);
        }

        //[Fact] DI issue?
        public void TestFetchData_WithCustomerData()
        {
            // Arrange
            IMapper mapper = Extensions.MockMapper();
            var data = Extensions.GenerateDB();
            var fdata = mapper.Map<PagedCollection<CustomerDataModel>, CustomerCollection>(data);
            var customerService = Extensions.MockCustomerServiceForGet();
            Services.AddSingleton(customerService);

            // Act
            var cut = RenderComponent<CustomerTable>();

            // Assert
            var expectedDataTable = RenderComponent<CustomerTable>((nameof(CustomerTable.Customers), fdata.Items));
            cut.MarkupMatches(expectedDataTable);
        }
    }
}
