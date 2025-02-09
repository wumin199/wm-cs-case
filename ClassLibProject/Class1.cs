namespace UtilityLibraries;

public static class StringLibrary
{
  // 扩展方法使你能够向现有类型“添加”方法，而无需创建新的派生类型、重新编译或以其他方式修改原始类型。
  // StartsWithUpper 以扩展方法的形式进行实现，这样就可以将其作为 String 类成员进行调用
  public static bool StartsWithUpper(this string? str)
  {
    if (string.IsNullOrWhiteSpace(str))
      return false;

    char ch = str[0];
    return char.IsUpper(ch);
  }
}