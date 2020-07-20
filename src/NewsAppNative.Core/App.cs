using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using NewsAppNative.Core.Rest;
using NewsAppNative.Core.Rest.Implementations;
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

            Mvx.IoCProvider.RegisterType<INewsRepositoryService, NewsRepository>();
            Mvx.IoCProvider.RegisterType<IStorageService, Storage>();
            Mvx.IoCProvider.RegisterType<IRestClient, RestClient>();
            RealmConfigurationBase config = RealmConfiguration.DefaultConfiguration;
            config.SchemaVersion = 1;

            RegisterAppStart<MainViewModel>();
        }
    }
}
