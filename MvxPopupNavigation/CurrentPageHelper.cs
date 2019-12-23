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

using System.Linq;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace MvxPopupNavigation
{
    public static class CurrentPageHelper
    {
        public static MvxContentPage GetCurrentPage()
        {
            var modalStack = Application.Current.NavigationProxy.ModalStack;

            Page currentPage;

            if (modalStack.Any())
            {
                currentPage = GetNextPage(modalStack.First());
            }
            else
            {
                var rootPage = Application.Current.MainPage;
                currentPage = GetNextPage(rootPage);
            }

            // Must be able to get ANY kind of Page I think

            return currentPage as MvxContentPage;
        }

        private static Page GetNextPage(Page page)
        {
            if (page is NavigationPage navigationPage)
            {
                return GetNextPage(navigationPage.CurrentPage);
            }
            else if (page is MasterDetailPage masterDetailPage)
            {
                return GetNextPage(masterDetailPage.Detail);
            }
            else if (page is TabbedPage tabbedPage)
            {
                return GetNextPage(tabbedPage.CurrentPage);
            }
            else if (page is CarouselPage carouselPage)
            {
                return GetNextPage(carouselPage.CurrentPage);
            }
            else
            {
                return page;
            }
        }
    }
}
