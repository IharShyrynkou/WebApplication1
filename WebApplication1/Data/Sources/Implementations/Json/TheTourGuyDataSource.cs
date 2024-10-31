using AutoMapper;
using WebApplication1.Data.Sources.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data.Data_Sources
{
    /// <summary>
    /// Source Class Json Configuration
    /// </summary>
    public static partial class JsonDataSourceConfiguration
    {
        public const string TheTourGuyDataFilePath = "TheTourGuyData.json";
        public const string TheTourGuySourceCode = "TheTourGuy";
    }

    /// <summary>
    /// Source Classs Registration module
    /// </summary>
    public static class TheTourGuyDataSourceRegistrator
    {
        public static IServiceCollection AddTheTourGuyDataRepository(this IServiceCollection services)
            => services
                .AddAutoMapper(typeof(TheTourGuyDataMappingProfile))
                .AddScoped<ProductDataSet, TheTourGuyDataSource>();
    }

    /// <summary>
    /// Source Class DataSource Configuration
    /// </summary>
    public class TheTourGuyDataSource : ProductFileDataSet<TheTourGuyData, TheTourGuyDataProductData>
    {
        public TheTourGuyDataSource(IMapper mapper, ILogger<TheTourGuyDataSource> logger, IConfiguration config)
            : base(mapper, logger, config) { }

        protected override string DefaultFileName => JsonDataSourceConfiguration.TheTourGuyDataFilePath;
        protected override string SourceCode => JsonDataSourceConfiguration.TheTourGuySourceCode;

        protected override Func<TheTourGuyData, TheTourGuyDataProductData[]> GetDataFunction => (data) => data.data;
    }

    /// <summary>
    /// Source Class Mapping Profile
    /// </summary>
    public class TheTourGuyDataMappingProfile : Profile
    {
        public TheTourGuyDataMappingProfile()
        {
            CreateMap<TheTourGuyDataProductData, ProductDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.title))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.regularPrice))
                    .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.maximumGuests))
                    .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => JsonDataSourceConfiguration.TheTourGuyDataFilePath))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                    .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => JsonDataSourceConfiguration.JsonDestination)).ReverseMap();
        }
    }

    /// <summary>
    /// Source Class
    /// </summary>
    public class TheTourGuyData
    {
        public TheTourGuyDataProductData[] data { get; set; }
    }

    /// <summary>
    /// Source Class Schema
    /// </summary>
    public class TheTourGuyDataProductData
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float averageRating { get; set; }
        public float regularPrice { get; set; }
        public float discountPrice { get; set; }
        public int maximumGuests { get; set; }
        public TheTourGuyDataProductImage[] images { get; set; }
    }

    public class TheTourGuyDataProductImage
    {
        public string url { get; set; }
        public int displayOrder { get; set; }
    }
}
