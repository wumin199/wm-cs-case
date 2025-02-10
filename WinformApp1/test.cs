using System.ComponentModel;
using System.Windows.Forms;

namespace Test;


// ExpandableObjectConverter 类型转换器的主要作用是让对象在属性网格（如 PropertyGrid 控件）中可以展开和编辑其属性。这样，你不需要手动编写代码来处理每个属性的显示和编辑逻辑，PropertyGrid 控件会自动处理这些工作。
[TypeConverter(typeof(ExpandableObjectConverter))]
public class Person : INotifyPropertyChanged
{
  private string name;
  private int age;

  public event PropertyChangedEventHandler PropertyChanged;

  public string Name
  {
    get => name;
    set
    {
      if (name != value)
      {
        name = value;
        OnPropertyChanged(nameof(Name));
      }
    }
  }

  public int Age
  {
    get => age;
    set
    {
      if (age != value)
      {
        age = value;
        OnPropertyChanged(nameof(Age));
      }
    }
  }

  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}