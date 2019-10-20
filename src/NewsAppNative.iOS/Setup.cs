using MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using NewsAppNative.Core;
using NewsAppNative.Core.PlatformSpecific;
using NewsAppNative.Core.Services;
using NewsAppNative.iOS.PlatformSpecific;

namespace NewsAppNative.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterType<IHttpClient, IosHttpClient>();
            Mvx.IoCProvider.RegisterType<IDialogService, DialogService>();
        }
    }
}
