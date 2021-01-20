using BlazorApp;
using System;
using Xunit;

namespace UnitTests
{
    public class CustomerDataGenerator : TheoryData<Guid?, CustomerViewModel>
    {
        public CustomerDataGenerator()
        {
            Add(null, null);
            Add(null, new CustomerViewModel());
            Add(Guid.Empty, new CustomerViewModel());
            Add(Guid.Empty, null);
            Add(Guid.NewGuid(), null);
            Add(Guid.NewGuid(), new CustomerViewModel());
            Add(Guid.NewGuid(), new CustomerViewModel { Id = Guid.NewGuid() });
        }
    }
}
