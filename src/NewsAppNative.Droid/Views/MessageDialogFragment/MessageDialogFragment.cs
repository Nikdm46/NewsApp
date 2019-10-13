using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace NewsAppNative.Droid.Views.MessageDialogFragment
{
    public sealed class MessageDialogFragment : AppCompatDialogFragment
    {
        private Action _action;
        private string _titleText;
        private string _messageText;
        private string _buttonText;

        public MessageDialogFragment()
        {
        }

        public MessageDialogFragment(string title, string message, string btnText)
        {
            _titleText = title;
            _messageText = message;
            _buttonText = btnText;
            Cancelable = false;
        }

        public MessageDialogFragment(Action action, string title, string message, string btnText)
            : this(title, message, btnText)
        {
            _action = action;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
            {
                _titleText = savedInstanceState.GetString("titleText");
                _messageText = savedInstanceState.GetString("messageText");
                _buttonText = savedInstanceState.GetString("buttonText");
            }
            return base.OnCreateDialog(savedInstanceState);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("titleText", _titleText);
            outState.PutString("messageText", _messageText);
            outState.PutString("buttonText", _buttonText);
            base.OnSaveInstanceState(outState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.dialog_alert, container, false);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

            TextView title = view.FindViewById<TextView>(Resource.Id.dialog_alert_title);
            title.Text = _titleText;

            TextView message = view.FindViewById<TextView>(Resource.Id.dialog_alert_message);
            message.Text = _messageText;

            Button okButton = view.FindViewById<Button>(Resource.Id.dialog_alert_okButton);
            okButton.Click += CloseDialog;
            okButton.Text = _buttonText;

            return view;
        }

        public override void Dismiss()
        {
            Button okButton = Dialog.FindViewById<Button>(Resource.Id.dialog_alert_okButton);
            okButton.Click -= CloseDialog;
            base.Dismiss();
        }

        private void CloseDialog(object sender, EventArgs eventArgs)
        {
            _action?.Invoke();
            Dialog?.Dismiss();
        }
    }
}
