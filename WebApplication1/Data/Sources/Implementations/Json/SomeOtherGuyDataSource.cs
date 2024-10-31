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
        public const string SomeOtherGuyDataFilePath = "SomeOtherGuyData.json";
        public const string SomeOtherGuySourceCode = "SomeOtherGuy";
    }

    /// <summary>
    /// Source Classs Registration module
    /// </summary>
    public static class SomeOtherGuyDataSourceRegistrator
    {
        public static IServiceCollection AddSomeOtherGuyRepository(this IServiceCollection services)
            => services
                .AddAutoMapper(typeof(SomeOtherGuyDataMappingProfile))
                .AddScoped<ProductDataSet, SomeOtherGuyDataSource>();
    }

    /// <summary>
    /// Source Class DataSource Configuration
    /// </summary>
    public class SomeOtherGuyDataSource : ProductFileDataSet<SomeOtherGuyData, SomeOtherGuyProductData>
    {
        public SomeOtherGuyDataSource(IMapper mapper, ILogger<SomeOtherGuyDataSource> logger, IConfiguration config)
            : base(mapper, logger, config) { }

        protected override string DefaultFileName => JsonDataSourceConfiguration.SomeOtherGuyDataFilePath;
        protected override string SourceCode => JsonDataSourceConfiguration.SomeOtherGuySourceCode;

        protected override Func<SomeOtherGuyData, SomeOtherGuyProductData[]> GetDataFunction => (data) => data.Products;
    }


    /// <summary>
    /// Source Class Mapping Profile
    /// </summary>
    public class SomeOtherGuyDataMappingProfile : Profile
    {
        public SomeOtherGuyDataMappingProfile()
        {
            CreateMap<SomeOtherGuyProductData, ProductDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                    .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.capacity))
                    .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => JsonDataSourceConfiguration.SomeOtherGuyDataFilePath))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.productDescription))
                    .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => JsonDataSourceConfiguration.JsonDestination)).ReverseMap();
        }
    }

    /// <summary>
    /// Source Classs
    /// </summary>
    public class SomeOtherGuyData
    {
        public SomeOtherGuyProductData[] Products { get; set; }
    }

    /// <summary>
    /// Source Class Schema
    /// </summary>
    public class SomeOtherGuyProductData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string productDescription { get; set; }
        public TheBigGuyProductDataRatingStatistics ratingStatistics { get; set; }
        public float price { get; set; }
        public int discountPercentage { get; set; }
        public int capacity { get; set; }
        public string[] imageUrls { get; set; }
    }

    public class TheBigGuyProductDataRatingStatistics
    {
        public int totalNumberOfReviews { get; set; }
        public int totalRating { get; set; }
    }

}