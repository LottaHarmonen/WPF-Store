using Labb3ProgTemplate.Enums;
namespace Labb3ProgTemplate.DataModels.Products;


public abstract class Product
{
    public string Name { get; set; }

    public double Price { get; set; }

    public abstract ProductTypes ProductTypes { get; set; }

    public string ImageFilePath { get; set; }

    protected Product(string name, double price, string imageFilePath)
    {
        Name = name;
        Price = price;
        ImageFilePath = imageFilePath;
    }
}