using System;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DXTestCase.ViewModels
{
  public partial class ExampleViewModel : ObservableObject
  {
    // 1. 基本属性
    [ObservableProperty]
    private string _name;

    // 2. 带依赖的属性 - 姓名相关
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    private string _firstName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [NotifyPropertyChangedFor(nameof(DisplayName))]
    private string _lastName;

    // 3. 只读计算属性
    public string FullName => $"{FirstName} {LastName}".Trim();
    public string DisplayName => string.IsNullOrEmpty(FullName) ? "未命名" : FullName;

    // 4. 带验证的属性 - 邮箱相关
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEmailValid))]
    [NotifyPropertyChangedFor(nameof(CanSave))]
    private string _email;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEmailValid))]
    [NotifyPropertyChangedFor(nameof(CanSave))]
    private string _confirmEmail;

    // 5. 验证状态属性
    public bool IsEmailValid => !string.IsNullOrEmpty(Email) &&
                              !string.IsNullOrEmpty(ConfirmEmail) &&
                              Email == ConfirmEmail &&
                              IsValidEmailFormat(Email);

    // 6. 带验证的属性 - 年龄相关
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAgeValid))]
    [NotifyPropertyChangedFor(nameof(CanSave))]
    private int _age;

    // 7. 验证状态属性
    public bool IsAgeValid => Age >= 0 && Age <= 150;

    // 8. 带验证的属性 - 密码相关
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPasswordValid))]
    [NotifyPropertyChangedFor(nameof(CanSave))]
    private string _password;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPasswordValid))]
    [NotifyPropertyChangedFor(nameof(CanSave))]
    private string _confirmPassword;

    // 9. 验证状态属性
    public bool IsPasswordValid => !string.IsNullOrEmpty(Password) &&
                                 !string.IsNullOrEmpty(ConfirmPassword) &&
                                 Password == ConfirmPassword &&
                                 Password.Length >= 8;

    // 10. 综合验证状态
    public bool CanSave => IsEmailValid && IsAgeValid && IsPasswordValid;

    // 11. 带验证的命令
    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
      // 保存逻辑
      Console.WriteLine($"保存用户信息：{FullName}");
    }

    // 12. 辅助方法
    private bool IsValidEmailFormat(string email)
    {
      if (string.IsNullOrEmpty(email)) return false;
      try
      {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
      }
      catch
      {
        return false;
      }
    }

    // 13. 带验证的属性 - 数值范围
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsValueInRange))]
    private double _value;

    public bool IsValueInRange => Value >= 0 && Value <= 100;

    // 14. 带验证的属性 - 日期相关
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDateValid))]
    private DateTime _birthDate;

    public bool IsDateValid => BirthDate <= DateTime.Now &&
                             BirthDate >= DateTime.Now.AddYears(-150);

    // 15. 带验证的属性 - 自定义格式
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPhoneValid))]
    private string _phoneNumber;

    public bool IsPhoneValid => !string.IsNullOrEmpty(PhoneNumber) &&
                              Regex.IsMatch(PhoneNumber, @"^1[3-9]\d{9}$");

    // 16. 带验证的属性 - 文件路径
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFilePathValid))]
    private string _filePath;

    public bool IsFilePathValid => !string.IsNullOrEmpty(FilePath) &&
                                 System.IO.Path.IsPathRooted(FilePath);

    // 17. 带验证的属性 - 网络地址
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsUrlValid))]
    private string _url;

    public bool IsUrlValid => !string.IsNullOrEmpty(Url) &&
                            Uri.TryCreate(Url, UriKind.Absolute, out _);

    // 18. 带验证的属性 - 数值精度
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPrecisionValid))]
    private decimal _amount;

    public bool IsPrecisionValid => Amount >= 0 &&
                                  Amount <= 999999.99m &&
                                  decimal.Round(Amount, 2) == Amount;
  }
}