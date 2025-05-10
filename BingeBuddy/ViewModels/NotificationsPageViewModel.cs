using System.Collections.ObjectModel;

public class NotificationItem
{
    public string Icon { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string Time { get; set; }
    public bool IsHighlighted { get; set; }
}

public class NotificationsPageViewModel
{
    public ObservableCollection<NotificationItem> Notifications { get; set; }

    public NotificationsPageViewModel()
    {
        Notifications = new ObservableCollection<NotificationItem>
        {
            new NotificationItem { Icon="icon_leaf.png", Title="The Social Network", Message="Rate Now!", Time="12:00 AM", IsHighlighted=false },
            new NotificationItem { Icon="icon_offer.png", Title="The Amazing Spiderman", Message="Rate Now!", Time="2 Days Ago", IsHighlighted=true },
            new NotificationItem { Icon="icon_delivery.png", Title="Lilo and Stitch", Message="Rate Now!", Time="12:00 AM", IsHighlighted=false },
            // Add more dummy data as needed
        };
    }
}
