using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;


namespace Labb3ProgTemplate.Managerrs;

public static class UserManager
{

    //lista med users, fyll med users från fil
    private static readonly IEnumerable<User>? _users = new List<User>()
    {
        new Admin("Lotta", "123"),
        new Customer("Alexander", "321")
    };


    //sätt loggedin user till _currentUser
    private static User _currentUser;

    public static IEnumerable<User>? Users => _users;

    public static User CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            CurrentUserChanged?.Invoke();
        }
    }

    public static event Action CurrentUserChanged;

    public static event Action UserListChanged;


    public static bool IsAdminLoggedIn
    {
        get
        {
            if (CurrentUser is null)
            {
                return false;
            }
            return CurrentUser.Type is UserTypes.Admin;
        }
    }


    public static bool IsCustomerLoggedIn
    {
        get
        {
            if (CurrentUser is null)
            {
                return false;
            }
            return CurrentUser.Type is UserTypes.Customer;
        }
    }

    public static void Register(string username, string password, UserTypes usertype)
    {
        if (usertype is UserTypes.Admin)
        {
            var newAdmin = new Admin(username, password);
            ((List<User>)Users).Add(newAdmin);
        }
        else if (usertype is UserTypes.Customer)
        {
            var newCustomer = new Customer(username, password);
            ((List<User>)Users).Add(newCustomer);
        }
    }

    public static void ChangeCurrentUser(string name, string password, UserTypes usertype)
    {

        foreach (User user in Users)
        {
            if (user.Name == name && user.Password == password)
            {
                CurrentUser = user;
            }
        }
    }


    public static void LogOut()
    {
        SaveUsersToFile();
        ProductManager.SaveProductsToFile();
        CurrentUser = null;

    }


    public static async Task SaveUsersToFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3.json");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "Users.json");

        var json = JsonSerializer.Serialize(Users, new JsonSerializerOptions() { WriteIndented = true });


        await using StreamWriter sw = new StreamWriter(path);

        await sw.WriteLineAsync(json);

    }

    public static async Task LoadUsersFromFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3.json");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "Users.json");

        if (!File.Exists(path))
        {
            await SaveUsersToFile();
        }

        else if (File.Exists(path))
        {
            ((List<User>)Users).Clear();

            var text = string.Empty;
            string line = string.Empty;

            StreamReader sr = new StreamReader(path);

            while ((line = sr.ReadLine()) != null)
            {
                text += line;
            }

            using (var jsonDoc = JsonDocument.Parse(text))
            {
                if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                    {
                        User a;
                        switch (jsonElement.GetProperty("Type").GetInt32())
                        {
                            case 0:
                                a = jsonElement.Deserialize<Admin>();
                                ((List<User>)Users).Add(a);
                                break;

                            case 1:
                                a = jsonElement.Deserialize<Customer>();
                                ((List<User>)Users).Add(a);


                                if (jsonElement.GetProperty("Cart").ValueKind == JsonValueKind.Array)
                                {
                                    foreach (var jsonElementt in jsonElement.GetProperty("Cart").EnumerateArray())
                                    {
                                        Product b;
                                        switch (jsonElementt.GetProperty("ProductTypes").GetInt32())
                                        {
                                            case 0:
                                                b = jsonElementt.Deserialize<Dairy>();
                                                a.Cart.Add(b);
                                                break;
                                            case 1:
                                                b = jsonElementt.Deserialize<Meat>();
                                                a.Cart.Add(b);
                                                break;
                                            case 2:
                                                b = jsonElementt.Deserialize<Fruit>();
                                                a.Cart.Add(b);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}