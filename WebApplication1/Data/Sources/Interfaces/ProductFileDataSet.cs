using AutoMapper;
using WebApplication1.Data.Data_Sources;
using WebApplication1.Models;

namespace WebApplication1.Data.Sources.Interfaces
{
    public abstract class ProductFileDataSet<T, TT> : ProductDataSet
        where T : class
        where TT : class
    {
        protected readonly string _baseFilePath;
        private readonly ILogger<ProductDataSet> _logger;

        protected ProductFileDataSet(IMapper mapper, ILogger<ProductDataSet> logger, IConfiguration config) : base(mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _baseFilePath = GetFilePath(config);
        }

        protected virtual string GetFilePath(IConfiguration config)
        {
            var configSection = JsonDataSourceConfiguration.GetJsonFileDataSourcesConfiguration(config)
                .FirstOrDefault(c => c.SourceCode == SourceCode);

            if (configSection == null){
                _logger.Log(LogLevel.Error, "{1} configuration not presented in settings!",
                    nameof(TheBigGuyDataSource));

                return Path.Combine(JsonDataSourceConfiguration.JsonDataStorageDefaultPath, DefaultFileName);
            }

            if (configSection.Path == null)
                _logger.Log(LogLevel.Error, "{1} {2}  configuration not presented in settings!",
                    nameof(TheBigGuyDataSource),
                    nameof(JsonFileConfigSection.Path));

            if (configSection.FileName == null)
                _logger.Log(LogLevel.Error, "{1} {2} configuration not presented in settings!",
                    nameof(TheBigGuyDataSource),
                    nameof(JsonFileConfigSection.FileName));

            return configSection.Path == null || configSection.FileName == null
                ? Path.Combine(JsonDataSourceConfiguration.JsonDataStorageDefaultPath, DefaultFileName)
                : Path.Combine(configSection.Path, configSection.FileName);
        }

        protected abstract Func<T, TT[]> GetDataFunction {  get; }


        public override ProductDTO[] Products => MapJson();

        protected virtual ProductDTO[] MapJson()
        {
            var bigGuyData = GenericSource.LoadJsonFile<T>(_baseFilePath);

            return _mapper.Map<TT[],ProductDTO[]>(GetDataFunction.Invoke(bigGuyData));
        }

        protected abstract string DefaultFileName { get; }
        protected abstract string SourceCode { get; }
    }
}
