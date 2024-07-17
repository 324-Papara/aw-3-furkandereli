using AutoMapper;
using Para.Data.Domain;
using Para.Schema;

namespace Para.Bussiness;

public class MapperConfig : Profile
{

    public MapperConfig()
    {
        CreateMap<Customer, CustomerResponse>().ReverseMap();
        CreateMap<CustomerRequest, Customer>().ReverseMap();
        
        CreateMap<CustomerAddress, CustomerAddressResponse>().ReverseMap();
        CreateMap<CustomerAddressRequest, CustomerAddress>().ReverseMap();
        
        CreateMap<CustomerPhone, CustomerPhoneResponse>().ReverseMap();
        CreateMap<CustomerPhoneRequest, CustomerPhone>().ReverseMap();
        
        CreateMap<CustomerDetail, CustomerDetailResponse>().ReverseMap();
        CreateMap<CustomerDetailRequest, CustomerDetail>().ReverseMap();
    }
}