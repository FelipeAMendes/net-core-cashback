using AutoMapper;
using ICI.Cashback.Application.ViewModels.Purchase;
using ICI.Cashback.Application.ViewModels.Reseller;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Extensions;

namespace ICI.Cashback.Application.AutoMapper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Purchase, PurchaseViewModel>()
				.ReverseMap()
				.ForMember(dest => dest.Reseller, opt => opt.Ignore());
			CreateMap<Purchase, PurchaseListViewModel>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()))
				.ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToShortDateString()))
				.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value.ToString("C")))
				.ForMember(dest => dest.ResellerName, opt => opt.MapFrom(src => src.Reseller.Name));
			CreateMap<Reseller, ResellerViewModel>()
				.ReverseMap();
			CreateMap<Reseller, ResellerListViewModel>()
				.ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.FormatCpf()));
		}
	}
}
