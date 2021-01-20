using AutoMapper;
using BlazorApp;
using DataAccessLibrary;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public static class Extensions
    {
        public static CustomerService MockCustomerServiceForGet()
        {
            IMapper mapper = MockMapper();
            var mock = new Mock<ICustomerContext>();
            mock.Setup(p => p.GetCustomersAsync(It.IsAny<PaginationDTO>()))
                .Returns(Task.FromResult(GenerateDB()));
            CustomerService customerService = new CustomerService(mock.Object, mapper);
            return customerService;
        }

        public static CustomerService MockCustomerServiceForPost()
        {
            IMapper mapper = MockMapper();
            var mock = new Mock<ICustomerContext>();
            mock.Setup(p => p.InsertCustomerAsync(It.IsAny<CustomerDataModel>()))
                .Returns(Task.FromResult(GenerateCustomer()));
            CustomerService customerService = new CustomerService(mock.Object, mapper);
            return customerService;
        }

        public static CustomerService MockCustomerServiceForUpdate()
        {
            IMapper mapper = MockMapper();
            var mock = new Mock<ICustomerContext>();
            mock.Setup(p => p.UpdateCustomerAsync(It.IsAny<Guid>(), It.IsAny<CustomerDataModel>()))
                .Returns(Task.FromResult(true));
            CustomerService customerService = new CustomerService(mock.Object, mapper);
            return customerService;
        }

        public static CustomerService MockCustomerServiceForDelete()
        {
            IMapper mapper = MockMapper();
            var mock = new Mock<ICustomerContext>();
            mock.Setup(p => p.DeleteCustomerAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));
            CustomerService customerService = new CustomerService(mock.Object, mapper);
            return customerService;
        }

        private static IMapper MockMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomerProfile());
            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }

        public static PagedCollection<CustomerDataModel> GenerateDB()
        {
            return new PagedCollection<CustomerDataModel>
            {
                Items = new List<CustomerDataModel>
                    { new CustomerDataModel
                            {
                                CompanyName = "New Company 1",
                                Country = "Greece"
                            },
                    { new CustomerDataModel
                            {
                                CompanyName = "New Company 2",
                                Country = "Cyprus"
                            }
                    },
                },
                CurrentPage = 1,
                TotalCount = 25,
                TotalPages = 3
            };
        }

        public static CustomerViewModel GenerateCustomer(bool hasId = true)
        {
            return new CustomerViewModel
            {
                Id = hasId ? Guid.NewGuid() : (Guid?) null,
                CompanyName = "New Company",
                Country = "Greece"
            };
        }
    }
}
