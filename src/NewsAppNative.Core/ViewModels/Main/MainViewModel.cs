using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using NewsAppNative.Core.ViewModels.News;

namespace NewsAppNative.Core.ViewModels.Main
{
    public class MainViewModel : MvxNavigationViewModel
    {
        private IMvxAsyncCommand ShowInitialViewModelsCommand { get; }
        
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {            
            ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
        }
        public override async Task Initialize()
        {
            await base.Initialize();
        }
        private async Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                NavigationService.Navigate<NewsViewModel>(),
            };
            await Task.WhenAll(tasks);
        }
    }
}
