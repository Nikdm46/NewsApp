using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using NewsAppNative.Core;
using NewsAppNative.Core.Services;
using NewsAppNative.Droid.PlatformSpecific;

namespace NewsAppNative.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterType<IDialogService, DialogService>();
        }
    }
}
