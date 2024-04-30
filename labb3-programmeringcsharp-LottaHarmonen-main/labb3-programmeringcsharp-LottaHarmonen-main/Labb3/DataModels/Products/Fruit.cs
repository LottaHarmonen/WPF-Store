using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public class Fruit : Product
{
    public Fruit(string name, double price, string imageFilePath) : base(name, price, imageFilePath)
    {
        ProductTypes = ProductTypes.Fruit;
    }

    public override ProductTypes ProductTypes { get; set; }
}