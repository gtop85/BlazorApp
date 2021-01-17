using System.Collections.Generic;

namespace BlazorApp
{
    public class CustomerCollection
    {
        public List<CustomerViewModel> Items;
        public int TotalCount;
        public int TotalPages;
        public int CurrentPage;
    }
}
