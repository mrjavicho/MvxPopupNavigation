//  Copyright 2019  Javier Huamanchumo <javier.huamanchumo@gmail.com>

//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at

//      http://www.apache.org/licenses/LICENSE-2.0

//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Forms.Presenters;
using MvvmCross.Forms.Views;
using MvvmCross.Navigation;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace MvxPopupNavigation
{
    public class CustomMvxFormsPagePresenter : MvxFormsPagePresenter
    {
        protected readonly IMvxFormsPagePresenter _mvxPagePresenter;
        
        public CustomMvxFormsPagePresenter(IMvxFormsViewPresenter platformPresenter) : base(platformPresenter)
        {
            _mvxPagePresenter = Mvx.IoCProvider.Resolve<IMvxFormsPagePresenter>();
        }

        public override void RegisterAttributeTypes()
        {
            base.RegisterAttributeTypes();
            AttributeTypesToActionsDictionary.Register<MvxPopUpPageAttribute>(ShowPopUpPage, ClosePopUpPage);
        }

        protected virtual MvxPage GetLastPage()
        {
            var topNavigationPage = _mvxPagePresenter.TopNavigationPage();
            return topNavigationPage?.Navigation?.NavigationStack?.OfType<MvxPage>().LastOrDefault();
        }

        protected virtual async Task<bool> ShowPopUpPage(Type view, MvxPopUpPageAttribute attribute,
                                              MvxViewModelRequest request)
        {
            try
            {
                var page = CreatePage(view, request, attribute);
                if (!(page is PopupPage popupPage))
                {
                    return false;
                }
                
                var popupNavInstance = Rg.Plugins.Popup.Services.PopupNavigation.Instance;
                GetLastPage()?.ViewModel?.ViewDisappearing();
                GetLastPage()?.ViewModel?.ViewDisappeared();
                await popupNavInstance.PushAsync(popupPage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual async Task<bool> ClosePopUpPage(IMvxViewModel viewModel, MvxPopUpPageAttribute attribute)
        {
            try
            {
                var popupNavInstance = Rg.Plugins.Popup.Services.PopupNavigation.Instance;
                if (popupNavInstance.PopupStack.Count > 0)
                {
                    await popupNavInstance.PopAsync();
                    GetLastPage()?.ViewModel?.ViewAppearing();
                    GetLastPage()?.ViewModel?.ViewAppeared();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
