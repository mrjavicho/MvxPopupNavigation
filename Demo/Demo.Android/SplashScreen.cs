using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;

namespace Demo.Droid
{
    [Activity(Label = "Demo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true , NoHistory = true)]
    public class SplashScreen : MvxFormsSplashScreenActivity<Setup, MvxApp, App>
    {
        public SplashScreen() : base(Resource.Layout.splash_activity)
        {

        }

        protected override async Task RunAppStartAsync(Bundle bundle)
        {            
            StartActivity(typeof(MainActivity));
        }
    }
}
