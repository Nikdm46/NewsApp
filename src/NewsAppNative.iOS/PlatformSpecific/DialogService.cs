using System.Linq;
using NewsAppNative.Core.Services;
using UIKit;

namespace NewsAppNative.iOS.PlatformSpecific
{
    public class DialogService : IDialogService
    {
        private UIViewController PresentationController
        {
            get
            {
                var rootNavigation = (UINavigationController)UIApplication.SharedApplication.KeyWindow.RootViewController;
                var lastVC = rootNavigation.ViewControllers.Last();
                if (lastVC.PresentedViewController != null)
                    return lastVC.PresentedViewController;
                if (lastVC.ModalViewController != null)
                    return lastVC.ModalViewController;
                return lastVC;
            }
        }
        public void Alert(string title, string message, string okButton)
        {
            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create(okButton, UIAlertActionStyle.Default, action =>
            {
                alertController.DismissViewControllerAsync(true);
            }));

            PresentationController?.PresentViewController(alertController, true, null);
        }
    }
}
