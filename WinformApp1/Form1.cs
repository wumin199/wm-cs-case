using System.ComponentModel;
using Test;
namespace WinformApp1;

public partial class Form1 : Form
{
  public Form1()
  {
    InitializeComponent();
  }
}


public partial class Test1 : Form
{
  private PropertyGrid propertyGrid;
  private Label lblName;
  private Label lblAge;

  public Test1()
  {
    propertyGrid = new PropertyGrid();
    {
      Dock = DockStyle.Left;
      Width = 200;
    }
    lblName = new Label
    {
      Location = new System.Drawing.Point(200, 20),
      AutoSize = true,
      // Text = "Name: Alice"
    };
    lblAge = new Label
    {
      Location = new System.Drawing.Point(200, 50),
      AutoSize = true,
      // Text = "Age: 30"
    };
    Controls.Add(propertyGrid);
    Controls.Add(lblName);
    Controls.Add(lblAge);

    var person = new Person { Name = "Alice", Age = 30 };
    propertyGrid.SelectedObject = person;

    person.PropertyChanged += Person_PropertyChanged;

    UpdateLabels(person);
  }

  private void Person_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {

    if (sender is Person person)
    {
      UpdateLabels(person);
    }
  }

  private void UpdateLabels(Person person)
  {
    lblName.Text = $"Name: {person.Name}";
    lblAge.Text = $"Age: {person.Age}";
  }
}