namespace BingeBuddy.Pages
{
    public partial class NotificationsPage : ContentPage
    {
        public NotificationsPage()
        {
            InitializeComponent();
            BindingContext = new NotificationsPageViewModel();
        }
    }

}
