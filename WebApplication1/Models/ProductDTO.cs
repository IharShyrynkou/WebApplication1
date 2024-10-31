using AutoMapper;

namespace WebApplication1.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public string Supplier { get; set; }

    }

    public class ProductResponseMappingProfile : Profile
    {
        public ProductResponseMappingProfile()
        {
            CreateMap<ProductDTO, ProductResponse>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination)).ReverseMap();
        }
    }
}
