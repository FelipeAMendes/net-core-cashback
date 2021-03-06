using AutoMapper;

namespace ICI.Cashback.Application.AutoMapper
{
	public class AutoMapperConfiguration
	{
		public static MapperConfiguration RegisterMappings()
		{
			return new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
		}
	}
}
