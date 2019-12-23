using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.Views;
using MvxPopupNavigation;

namespace Demo.Droid
{
    public class Setup : MvxFormsAndroidSetup<MvxApp, App>
    {
        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            var formsPagePresenter = new CustomMvxFormsPagePresenter(viewPresenter);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
            return formsPagePresenter;
        }

        protected override IMvxViewsContainer InitializeViewLookup(IDictionary<Type, Type> viewModelViewLookup)
        {
            var links = ViewModelLinker.GetExtraViewModelLinks();
            if (links.Any())
            {
                foreach (var pair in links)
                {
                    viewModelViewLookup.TryAdd(pair.Key, pair.Value);
                }
            }
            return base.InitializeViewLookup(viewModelViewLookup);
        }
    }
}
