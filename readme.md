## MvxPopupNavigation

This library allows to use [Rg.Plugin.Popup](https://github.com/rotorgames/Rg.Plugins.Popup) pages and navigation with [MvvmCross](https://www.mvvmcross.com/) in your Xamarin.Forms application.

1. Register CustomMvxFormsPagePresenter by overriding `CreateFormsPagePresenter` in the Android/iOS Setup class, this allows to use Rg.Plugin.Popup navigation.

   ```c#
   public class Setup : MvxFormsAndroidSetup<MvxApp, App>
   {
   	protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
   	{
   		var formsPagePresenter = new CustomMvxFormsPagePresenter(viewPresenter);
       Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
       return formsPagePresenter;
   	}
   }
   ```

   

2. Use MvxPopupPage as base for your desired popup pages (this class implements IMvxPage). 

   ```xaml
   <?xml version="1.0" encoding="UTF-8" ?>
   <mvxPopupNavigation:MvxPopupPage
       x:Class="Demo.Views.SamplePopupView"
       xmlns="http://xamarin.com/schemas/2014/forms"
       xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
       xmlns:mvxPopupNavigation="clr-namespace:MvxPopupNavigation;assembly=MvxPopupNavigation">
       <ContentPage.Content />
   </mvxPopupNavigation:MvxPopupPage>
   ```

   

3. Use the navigation attribute in the page C# declaration. 

   ```c#
   [MvxPopUpPage]
   public partial class SamplePopupView : MvxPopupPage
   {
   	public SamplePopupView()
   	{
   		InitializeComponent();
   	}
   }
   ```

   

   

   