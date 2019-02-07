using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace IllyaVirych.Droid
{
    public class Setup : MvxAppCompatSetup<IllyaVirych.Core.App>
    {
        protected override IMvxApplication CreateApp()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            return base.CreateApp();
        }
    }
}