
using AutoMapper;
using WebApplication1.Models;

namespace WebApplication1.Data.Data_Sources
{
    public static partial class JsonRepositoryConfiguration
    {
        public const string TheTourGuyDataFilePath = "TheTourGuyData.json";
    }

    public static class TheTourGuyDataSourceRegistrator
    {
        public static IServiceCollection AddTheTourGuyDataRepository(this IServiceCollection services)
            => services
                .AddAutoMapper(typeof(TheTourGuyDataMappingProfile))
                .AddScoped<ProductDataSet, TheTourGuyDataSource>();
    }

    public class TheTourGuyDataSource : ProductDataSet
    {
        public Type MapType => typeof(TheTourGuyDataMappingProfile);
        public TheTourGuyDataSource(IMapper mapper) : base(mapper){ }

        public override ProductDTO[] Products => MapJson();

        private ProductDTO[] MapJson()
        {
            var bigGuyData = GenericSource.LoadJsonFile<TheTourGuyData>(JsonRepositoryConfiguration.TheTourGuyDataFilePath);

            return _mapper.Map<ProductDTO[]>(bigGuyData.data);
        }
    }

    public class TheTourGuyDataMappingProfile : Profile
    {
        public TheTourGuyDataMappingProfile()
        {
            CreateMap<Datum, ProductDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src=> src.title))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.regularPrice))
                    .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.maximumGuests))
                    .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => JsonRepositoryConfiguration.TheTourGuyDataFilePath))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                    .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => JsonRepositoryConfiguration.JsonDestination)).ReverseMap();
                    ;
        }
    }
}
