using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Labb3ProgTemplate.DataModels.Products;

namespace Labb3ProgTemplate;

public class MainWindowContext : INotifyPropertyChanged
{
    private string _productName = "No item selected";

    public string ProductName
    {
        get { return _productName; }
        set
        {
            _productName = value;
            OnPropertyChanged("ProductName");
        }
    }

    private double _productPrice;

    public double ProductPrice
    {
        get { return _productPrice; }
        set
        {
            _productPrice = value;
            OnPropertyChanged("ProductPrice");
        }
    }

    

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}