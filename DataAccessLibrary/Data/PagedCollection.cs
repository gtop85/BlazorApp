using System.Collections.Generic;

namespace DataAccessLibrary
{
    public class PagedCollection<T>
    {
        public List<T> Items;
        public int TotalCount;
        public int TotalPages;
        public int CurrentPage;
    }
}

