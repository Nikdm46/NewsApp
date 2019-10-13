using Foundation;
using MvvmCross.Platforms.Ios.Core;
using NewsAppNative.Core;

namespace NewsAppNative.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}
