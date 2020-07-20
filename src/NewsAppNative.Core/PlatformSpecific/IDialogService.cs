namespace NewsAppNative.Core.Services
{
    public interface IDialogService
    {
        void Alert(string title, string message, string okButton = "OK");
    }
}
