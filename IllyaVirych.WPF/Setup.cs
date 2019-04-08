using MvvmCross.IoC;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllyaVirych.WPF
{
    public class Setup : MvxWpfSetup<IllyaVirych.Core.App>
    {
        public Setup() : base()
        {

        }
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }

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
