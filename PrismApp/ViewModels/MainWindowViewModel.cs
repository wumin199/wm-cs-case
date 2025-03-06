namespace PrismApp.ViewModels
{
  class MainWindowViewModel : BindableBase
  {
    private string _title = "Prism Application";
    private readonly IRegionManager regionManager;
    public string Title
    {
      get { return _title; }
      // 自动实现了INotifyPropertyChanged接口，当属性值发生变化时，会自动通知绑定的UI元素。
      //不是所有属性发生变化时，前端都发生了变化，只有实现了INotifyPropertyChanged接口的属性才会发生变化。
      //比如自定义的Title属性，如果不实现INotifyPropertyChanged接口，那么即使Title属性发生了变化，前端也不会发生变化。
      set { SetProperty(ref _title, value); }
    }

    public MainWindowViewModel(IRegionManager regionManager)
    {
      this.regionManager = regionManager;

      regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
    }
  }
}
