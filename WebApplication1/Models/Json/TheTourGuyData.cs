
using Newtonsoft.Json;
using WebApplication1;

public class TheTourGuyData
{
    public Datum[] data { get; set; }
}

public class Datum
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public float averageRating { get; set; }
    public float regularPrice { get; set; }
    public float discountPrice { get; set; }
    public int maximumGuests { get; set; }
    public Image[] images { get; set; }
}

public class Image
{
    public string url { get; set; }
    public int displayOrder { get; set; }
}

