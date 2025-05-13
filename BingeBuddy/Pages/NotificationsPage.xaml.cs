using Microsoft.Maui.Controls; // Add this namespace

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
