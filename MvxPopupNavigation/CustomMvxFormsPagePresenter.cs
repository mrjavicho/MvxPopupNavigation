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
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace MvxPopupNavigation
{
    public class CustomMvxFormsPagePresenter : MvxFormsPagePresenter
    {
        public CustomMvxFormsPagePresenter(IMvxFormsViewPresenter platformPresenter) : base(platformPresenter)
        {

        }

        public override void RegisterAttributeTypes()
        {
            base.RegisterAttributeTypes();
            AttributeTypesToActionsDictionary.Register<MvxPopUpPageAttribute>(ShowPopUpPage, ClosePopUpPage);
        }

        public async Task<bool> ShowPopUpPage(Type view, MvxPopUpPageAttribute attribute,
                                              MvxViewModelRequest request)
        {
            try
            {
                var page = CreatePage(view, request, attribute);
                if (page is PopupPage popupPage)
                {
                    var popupNavInstance = Rg.Plugins.Popup.Services.PopupNavigation.Instance;
                    var currentPage = CurrentPageHelper.GetCurrentPage();
                    currentPage.ViewModel.ViewDisappearing();

                    await popupNavInstance.PushAsync(popupPage);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ClosePopUpPage(IMvxViewModel viewModel, MvxPopUpPageAttribute attribute)
        {
            try
            {
                var popupNavInstance = Rg.Plugins.Popup.Services.PopupNavigation.Instance;
                if (popupNavInstance != null && popupNavInstance.PopupStack.Count > 0)
                {
                    await popupNavInstance.PopAsync();
                    var currentPage = CurrentPageHelper.GetCurrentPage();
                    currentPage.ViewModel.ViewAppearing();
                    currentPage.ViewModel.ViewAppeared();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
