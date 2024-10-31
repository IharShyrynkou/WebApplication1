using AutoMapper;

namespace WebApplication1.Data.Data_Sources
{
    public static partial class JsonDataSourceConfiguration
    {
        public const string JsonDestination = "JSON Document";
        public const string JsonDataStorageDefaultPath = "Resources\\Data Sources";


        public static ICollection<JsonFileConfigSection> GetJsonFileDataSourcesConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection("Data").GetSection("Sources")
               .GetSection("Json").GetSection("File").Get<List<JsonFileConfigSection>>() ?? new List<JsonFileConfigSection>();
        }
    }

    public partial class DataConfigurationSchema
    {
        JsonFileConfigSection[] File { get; set; }
    }

    public class JsonFileConfigSection
    {
        public string SourceCode { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }
}