using System;
using System.Collections.Generic;
using Demo.ViewModels;
using Demo.Views;

namespace Demo
{
    public static class ViewModelLinker
    {
        public static Dictionary<Type, Type> GetExtraViewModelLinks()
        {
            var viewModelViewLookup = new Dictionary<Type, Type>()
            {
                { typeof (HomeViewModel), typeof(HomeView)},                                
                { typeof (SamplePopupViewModel), typeof(SamplePopupView)},
            };
            return viewModelViewLookup;
        }
    }
}
