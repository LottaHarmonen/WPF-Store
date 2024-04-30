using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Data;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.DataModels.Users;

namespace Labb3ProgTemplate.Managerrs;

public static class ProductManager
{
    private static readonly IEnumerable<Product>? _products = new List<Product>()
    {
        new Fruit("Banana", 9.0, "../ImageFolder/Banana.jpg"),
        new Dairy("Milk", 15.0, "../ImageFolder/Milk.png"),
        new Meat("Pork", 110.5, "../ImageFolder/Pork.png"),
        new Fruit("Apple", 4.0, "../ImageFolder/Apple.jpg"),
        new Dairy("Yoghurt", 14.0, "../ImageFolder/Yoghurt.jpg"),
        new Meat("Beef", 190.0, "../ImageFolder/Beef.png")

    };

    public static IEnumerable<Product>? Products => _products;


    // Skicka detta efter att produktlistan ändrats eller lästs in
    public static event Action ProductListChanged;

    public static event Action CartListChanged;


    public static void AddProduct(Product product)
    {
        if (UserManager.CurrentUser is Admin)
        {
            var firstElement = Products.FirstOrDefault(item => item.Name == product.Name);

            if (firstElement is null)
            {
                ((List<Product>)Products).Add(product);
            }
            else
            {
                firstElement.Price = product.Price;
            }
        }
        else
        {
            UserManager.CurrentUser.Cart.Add(product);
            CartListChanged.Invoke();

        }

        ProductListChanged.Invoke();
    }


    public static void RemoveProduct(Product selectedproduct)
{
    if (UserManager.CurrentUser is Admin)
    {
        ((List<Product>)Products).Remove(selectedproduct);
        ProductListChanged.Invoke();
    }
    else
    {
        foreach (Product cartProduct in UserManager.CurrentUser.Cart)
        {
            if (cartProduct == selectedproduct)
            {
                UserManager.CurrentUser.Cart.Remove(cartProduct);
                CartListChanged.Invoke();
                return;
            }
        }
    }
}



    public static async Task SaveProductsToFile()
{
    var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3.json");
    Directory.CreateDirectory(directory);

    var path = Path.Combine(directory, "Products.json");
    var json = JsonSerializer.Serialize(Products, new JsonSerializerOptions() { WriteIndented = true });

    await using StreamWriter sw = new StreamWriter(path);


    await sw.WriteLineAsync(json);

}

public static async Task LoadProductsFromFile()
{

    var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3.json");
    Directory.CreateDirectory(directory);
    var path = Path.Combine(directory, "Products.json");

    if (!File.Exists(path))
    {
        SaveProductsToFile();
    }

    else if (File.Exists(path))
    {
        ((List<Product>)Products).Clear();
        var text = string.Empty;
        string line = String.Empty;
        StreamReader sr = new StreamReader(path);

        while ((line = sr.ReadLine()) != null)
        {
            text += line;
        }

        //var jsonList = await JsonSerializer.DeserializeAsync<List<User>>(sr.BaseStream);

        using (var jsonDoc = JsonDocument.Parse(text))
        {
            if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                {
                    Product a;
                    switch (jsonElement.GetProperty("ProductTypes").GetInt32())
                    {
                        case 0:
                            a = jsonElement.Deserialize<Dairy>();
                            ((List<Product>)Products).Add(a);
                            break;
                        case 1:
                            a = jsonElement.Deserialize<Meat>();
                            ((List<Product>)Products).Add(a);
                            break;
                        case 2:
                            a = jsonElement.Deserialize<Fruit>();
                            ((List<Product>)Products).Add(a);
                            break;
                    }
                }
            }
        }
    }
}
}