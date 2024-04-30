using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public class Meat : Product
{
    public Meat(string name, double price, string imageFilePath) : base(name, price, imageFilePath)
    {
        ProductTypes = ProductTypes.Meat;
    }

    public override ProductTypes ProductTypes { get; set; }
}