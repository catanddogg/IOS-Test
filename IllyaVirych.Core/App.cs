using AutoMapper;
using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using IllyaVirych.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;


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

            Mapper.Initialize(config =>
            {
                config.CreateMap<ChatData, ReceivedMessage>();
            });

            //RegisterAppStart<WPFLoginViewModel>();
            RegisterCustomAppStart<AppStart>();
        }
    }
}
