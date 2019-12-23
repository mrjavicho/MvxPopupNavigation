using System;
using Demo.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Demo
{
    public class MvxApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<HomeViewModel>();
        }
    }
}
