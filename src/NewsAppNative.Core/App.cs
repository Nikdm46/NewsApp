using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using NewsAppNative.Core.Services;
using NewsAppNative.Core.Services.Implementation;
using NewsAppNative.Core.ViewModels.Main;
using Realms;

namespace NewsAppNative.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterType<IRepositoryService, Repository>();
            Mvx.IoCProvider.RegisterType<IStorageService, Storage>();
            RealmConfigurationBase config = RealmConfiguration.DefaultConfiguration;
            config.SchemaVersion = 1;

            RegisterAppStart<MainViewModel>();
        }
    }
}
