using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace Demo.Views
{
    [MvxContentPagePresentation]    
    public partial class HomeView : MvxContentPage
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}
