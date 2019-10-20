using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using NewsAppNative.Core.ViewModels.FavoriteNews;
using NewsAppNative.Core.ViewModels.News;

namespace NewsAppNative.Core.ViewModels.Main
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public IMvxAsyncCommand ShowInitialViewModelsCommand { get; }
        private readonly IMvxNavigationService _navigationService;
        private async Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                NavigationService.Navigate<NewsViewModel>(),
                NavigationService.Navigate<FavoriteNewsViewModel>(),
            };
            await Task.WhenAll(tasks);
        }
        public override async void ViewCreated()
        {
            base.ViewCreated();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = Mvx.Resolve<IMvxNavigationService>();
            ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
        }
    }
}
