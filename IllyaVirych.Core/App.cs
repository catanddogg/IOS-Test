using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Services;
using IllyaVirych.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
               .EndingWith("Service")
               .AsInterfaces()
               .RegisterAsLazySingleton();

           
            //RegisterAppStart<MainViewModel>();
            RegisterCustomAppStart<AppStart>();
        }
    }
}
