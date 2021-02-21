using AutoMapper;
using CardApplication.Dto;

namespace CardApplication.Model.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>();
        }
    }
}
