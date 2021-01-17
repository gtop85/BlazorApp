using AutoMapper;
using DataAccessLibrary;

namespace BlazorApp
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDataModel, CustomerViewModel>();
            CreateMap<CustomerViewModel, CustomerDataModel>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<PagedCollection<CustomerDataModel>, CustomerCollection>();
        }
    }
}
