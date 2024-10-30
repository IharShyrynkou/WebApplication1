
using WebApplication1;

public class TheBigGuy
{
    public Productdata[] ProductData {  get; set; }
}

public class Productdata
{
    public Productdetaildata productDetailData { get; set; }
    public Price price { get; set; }
    public string[] photos { get; set; }
}

public class Productdetaildata
{
    public int id { get; set; }
    public string name { get; set; }
    public string productDescription { get; set; }
    public float averageStars { get; set; }
    public int capacity { get; set; }
}

public class Price
{
    public float amount { get; set; }
    public float appliedDiscount { get; set; }
}
