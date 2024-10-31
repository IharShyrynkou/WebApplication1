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
        public const string TheBigGuyDataFilePath = "TheBigGuy.json";
        public const string TheBigGuySourceCode = "TheBigGuy";
    }

    /// <summary>
    /// Source Classs Registration module
    /// </summary>
    public static class TheBigGuyDataSourceRegistrator
    {
        public static IServiceCollection AddTheBigGuyRepository(this IServiceCollection services)
            => services
                .AddAutoMapper(typeof(TheBigGuyDataMappingProfile))
                .AddScoped<ProductDataSet, TheBigGuyDataSource>();
    }

    /// <summary>
    /// Source Class DataSource Configuration
    /// </summary>
    public class TheBigGuyDataSource : ProductFileDataSet<TheBigGuyData, TheBigGuyProductData>
    {
        public TheBigGuyDataSource(IMapper mapper, ILogger<TheBigGuyDataSource> logger, IConfiguration config)
            : base(mapper, logger, config) { }

        protected override string DefaultFileName => JsonDataSourceConfiguration.TheBigGuyDataFilePath;
        protected override string SourceCode => JsonDataSourceConfiguration.TheBigGuySourceCode;

        protected override Func<TheBigGuyData, TheBigGuyProductData[]> GetDataFunction => (data) => data.ProductData;
    }

    /// <summary>
    /// Source Class Mapping Profile
    /// </summary>
    public class TheBigGuyDataMappingProfile : Profile
    {
        public TheBigGuyDataMappingProfile()
        {
            CreateMap<TheBigGuyProductData, ProductDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.productDetailData.id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.productDetailData.name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price.amount))
                    .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.productDetailData.capacity))
                    .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => JsonDataSourceConfiguration.TheBigGuyDataFilePath))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.productDetailData.productDescription))
                    .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => JsonDataSourceConfiguration.JsonDestination)).ReverseMap();
        }
    }


    /// <summary>
    /// Source Class
    /// </summary>
    public class TheBigGuyData
    {
        public TheBigGuyProductData[] ProductData { get; set; }
    }

    /// <summary>
    /// Source Class Schema
    /// </summary>
    public class TheBigGuyProductData
    {
        public TheBigGuyProductDataDetails productDetailData { get; set; }
        public TheBigGuyProductDataPrice price { get; set; }
        public string[] photos { get; set; }
    }

    public class TheBigGuyProductDataDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public string productDescription { get; set; }
        public float averageStars { get; set; }
        public int capacity { get; set; }
    }

    public class TheBigGuyProductDataPrice
    {
        public float amount { get; set; }
        public float appliedDiscount { get; set; }
    }
}