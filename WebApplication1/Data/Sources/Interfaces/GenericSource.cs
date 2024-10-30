using Newtonsoft.Json;

namespace WebApplication1
{
    public static class GenericSource
    {
        public static TSource LoadJsonFile<TSource>(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();

                return JsonConvert.DeserializeObject<TSource>(json);
            }
        }
    }
}