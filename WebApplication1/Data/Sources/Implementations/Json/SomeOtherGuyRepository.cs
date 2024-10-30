
using AutoMapper;
using WebApplication1.Models;

namespace WebApplication1.Data.Data_Sources
{
    public static partial class JsonRepositoryConfiguration
    {
        public const string SomeOtherGuyDataFilePath = "SomeOtherGuyData.json";
    }

    public static class SomeOtherGuySourceRegistrator
    {
        public static IServiceCollection AddSomeOtherGuyRepository(this IServiceCollection services) 
            => services
                .AddAutoMapper(typeof(SomeOtherGuyDataMappingProfile))
                .AddScoped<ProductDataSet, SomeOtherGuyDataSource>();
    }

    public class SomeOtherGuyDataSource : ProductDataSet
    {
        public Type MapType => typeof(SomeOtherGuyDataMappingProfile);
        public SomeOtherGuyDataSource(IMapper mapper) : base(mapper) { }

        public override ProductDTO[] Products => MapJson();

        private ProductDTO[] MapJson()
        {
            var bigGuyData = GenericSource.LoadJsonFile<SomeOtherGuyData>(JsonRepositoryConfiguration.SomeOtherGuyDataFilePath);

            return _mapper.Map<ProductDTO[]>(bigGuyData.Products);
        }
    }

    public class SomeOtherGuyDataMappingProfile : Profile
    {
        public SomeOtherGuyDataMappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                    .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.capacity))
                    .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => JsonRepositoryConfiguration.SomeOtherGuyDataFilePath))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.productDescription))
                    .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => JsonRepositoryConfiguration.JsonDestination)).ReverseMap();
            ;
        }
    }
}
