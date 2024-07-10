using WebPushNotificationService.Helpers;

namespace WebPushNotificationService.Extensions;

public static class PushNotificationApiExtensions
{

    public static WebApplication MapPushNotificationApi(this WebApplication app)
    {

        // Subscribe to notifications
        app.MapPut("/notifications/subscribe", async (
            HttpContext context,
            ServiceMonitorContext db,
            NotificationSubscription subscription) =>
        {

            subscription.Group = "NotificationTest";
            db.Add(subscription);
            await db.SaveChangesAsync();
            return Results.Ok(subscription);

        })
            .WithName("/notifications/subscribe")
            .WithOpenApi();
        app.MapPost("/notifications/send", async (
            HttpContext context,
            ServiceMonitorContext db
            ) =>
        {
            var subscriptions = db.NotificationSubscriptions.OrderByDescending(n => n.NotificationSubscriptionId);
            var notificationHelper = new NotificationHelper();
            foreach (var s in subscriptions)
            {
                try
                {
                    await notificationHelper.SendNotification(s);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error sending push notification to {s.NotificationSubscriptionId}: {ex.Message}\nRemoving subscription from database.");
                    db.Remove(s);
                }
            }
            await db.SaveChangesAsync();
            return Results.Ok();
        })
        .WithName("/notifications/Send")
        .WithOpenApi();
        return app;

    }



}