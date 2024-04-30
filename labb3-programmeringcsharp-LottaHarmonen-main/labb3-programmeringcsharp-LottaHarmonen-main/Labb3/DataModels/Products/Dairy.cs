using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public class Dairy : Product
{
    public Dairy(string name, double price, string imageFilePath) : base(name, price, imageFilePath)
    {
        ProductTypes = ProductTypes.Dairy;
    }

    public override ProductTypes ProductTypes { get; set; }
}