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

using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace MvxPopupNavigation
{
    public class MvxPopupPage : PopupPage, IMvxPage
    {
        public object DataContext
        {
            get => BindingContext.DataContext;
            set
            {
                if (value != null && !(_bindingContext != null && ReferenceEquals(DataContext, value)))
                    BindingContext = new MvxBindingContext(value);
            }
        }

        private IMvxBindingContext _bindingContext;
        public new IMvxBindingContext BindingContext
        {
            get
            {
                if (_bindingContext == null)
                    BindingContext = new MvxBindingContext(base.BindingContext);
                return _bindingContext;
            }
            set
            {
                _bindingContext = value;
                base.BindingContext = _bindingContext.DataContext;
            }
        }

        public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(nameof(ViewModel), typeof(IMvxViewModel), typeof(IMvxElement), default(MvxViewModel), BindingMode.Default, null, ViewModelChanged, null, null);

        internal static void ViewModelChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue != null)
            {
                if (bindable is IMvxElement element)
                    element.DataContext = newvalue;
                else
                    bindable.BindingContext = newvalue;
            }
        }

        public IMvxViewModel ViewModel
        {
            get => DataContext as IMvxViewModel;
            set
            {
                DataContext = value;
                SetValue(ViewModelProperty, value);
                OnViewModelSet();
            }
        }

        protected virtual void OnViewModelSet()
        {
            ViewModel?.ViewCreated();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel?.ViewDisappearing();
            ViewModel?.ViewDisappeared();
            ViewModel?.ViewDestroy();
        }
    }
}
