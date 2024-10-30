
public class SomeOtherGuyData
{
    public Product[] Products {  get; set; }
}

public class Product
{
    public int id { get; set; }
    public string name { get; set; }
    public string productDescription { get; set; }
    public Ratingstatistics ratingStatistics { get; set; }
    public float price { get; set; }
    public int discountPercentage { get; set; }
    public int capacity { get; set; }
    public string[] imageUrls { get; set; }
}

public class Ratingstatistics
{
    public int totalNumberOfReviews { get; set; }
    public int totalRating { get; set; }
}

