
using AutoMapper;
using WebApplication1.Models;

namespace WebApplication1.Data.Data_Sources
{
    public static partial class JsonRepositoryConfiguration
    {
        public const string TheBigGuyDataFilePath = "TheBigGuy.json";
    }

    public static class TheBigGuySourceRegistrator
    {
        public static IServiceCollection AddTheBigGuyRepository(this IServiceCollection services)
            => services
                .AddAutoMapper(typeof(TheBigGuyDataMappingProfile))
                .AddScoped<ProductDataSet, TheBigGuyDataSource>();
    }

    public class TheBigGuyDataSource : ProductDataSet
    {
        public Type MapType => typeof(TheBigGuyDataMappingProfile);
        public TheBigGuyDataSource(IMapper mapper) : base(mapper){ }

        public override ProductDTO[] Products => MapJson();

        private ProductDTO[] MapJson()
        {
            var bigGuyData = GenericSource.LoadJsonFile<TheBigGuy>(JsonRepositoryConfiguration.TheBigGuyDataFilePath);

            return _mapper.Map<ProductDTO[]>(bigGuyData.ProductData);
        }
    }

    public class TheBigGuyDataMappingProfile : Profile
    {
        public TheBigGuyDataMappingProfile()
        {
            CreateMap<Productdata, ProductDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.productDetailData.id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src=> src.productDetailData.name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price.amount))
                    .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.productDetailData.capacity))
                    .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => JsonRepositoryConfiguration.TheBigGuyDataFilePath))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.productDetailData.productDescription))
                    .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => JsonRepositoryConfiguration.JsonDestination)).ReverseMap();
                    ;
        }
    }
}
