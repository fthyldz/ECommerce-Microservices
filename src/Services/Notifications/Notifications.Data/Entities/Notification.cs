using Notifications.Data.Enums;
using Notifications.Data.Primitives;

namespace Notifications.Data.Entities;

public class Notification : BaseEntity
{
    public string Title { get; private set; } = default!;
    
    public string Content { get; private set; } = default!;

    public string To { get; private set; } = default!;
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public NotificationType NotificationType { get; private set; }

    private Notification()
    {
    }

    public static Notification Create(Guid id, string title, string content, string to, NotificationType notificationType)
    {
        var notification = new Notification
        {
            Id = id,
            Title = title,
            Content = content,
            To = to,
            NotificationType = notificationType
        };

        return notification;
    }
}