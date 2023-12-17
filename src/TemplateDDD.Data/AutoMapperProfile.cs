using AutoMapper;
using TemplateDDD.Data.Dtos.MonitorFailedHNITxn;
using TemplateDDD.Data.Entities;

namespace TemplateDDD.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<AddUserDto, ApplicationUser>()
            //    .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<MonitoringFailedHNITxn, MonitoringFailedHNITxnDto>().ReverseMap();
            CreateMap<CreateMonitoringFailedHNITxnDto, MonitoringFailedHNITxn>().ReverseMap();
            CreateMap<UpdateMonitoringFailedHNITxnDto, MonitoringFailedHNITxn>().ReverseMap();
        }
    }
}