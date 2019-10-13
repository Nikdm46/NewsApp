using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using NewsAppNative.Core.Services;

namespace NewsAppNative.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        public readonly IMvxNavigationService NavigationService;
        public readonly IDialogService DialogService;
        public readonly IRepositoryService RepositoryService;
        public readonly IMvxMessenger Messenger;

        public BaseViewModel()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            RepositoryService = Mvx.IoCProvider.Resolve<IRepositoryService>();
            DialogService = Mvx.IoCProvider.Resolve<IDialogService>();
            Messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        }
        public override async Task Initialize()
        {
            await base.Initialize();
        }
    }
}
