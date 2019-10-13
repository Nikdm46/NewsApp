using Android.App;
using Android.Support.V7.App;
using MvvmCross;
using MvvmCross.Platforms.Android;
using NewsAppNative.Core.Services;
using NewsAppNative.Droid.Views.MessageDialogFragment;

namespace NewsAppNative.Droid.PlatformSpecific
{
    public class DialogService : IDialogService
    {
        private readonly IMvxAndroidCurrentTopActivity _currentTopActivity;
        private AppCompatActivity CurrentActivity => _currentTopActivity.Activity as AppCompatActivity;

        public DialogService()
        {
            _currentTopActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
        }

        public void Alert(string title, string message, string okButton)
        {
            Application.SynchronizationContext.Post(ignored =>
            {
                if (CurrentActivity == null)
                    return;
                var dialogFragment = new MessageDialogFragment(title, message, okButton);
                CurrentActivity.SupportFragmentManager
                               .BeginTransaction()
                               .Add(dialogFragment, nameof(MessageDialogFragment))
                               .CommitAllowingStateLoss();
            }, null);
        }
    }
}
