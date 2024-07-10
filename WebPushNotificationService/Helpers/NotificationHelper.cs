
namespace WebPushNotificationService.Helpers
{
    public class NotificationHelper
    {
        public async Task SendNotification(NotificationSubscription subscription)
        {
            var publicKey = "BAV_5pZt8iVZVQZ8mvH2tBoA8UWd6WYlnh1uD48JSS8IEjfasLvs3VL0ELTVyECwXrO5RbToVsdGUtjl5nWpMvA";
            var privateKey = "4fJbbLHPoUzzJWuFC9XmIHTU4dNXVRUQNQ00HsfVUlE";
            /*
             * 
             * Those keys can be acquired with help of online generators (https://vapidkeys.com/, https://www.attheminute.com/vapid-key-generator).
             * 
             */

            var pushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);
            var vapidDetails = new VapidDetails("mailto:someone@somewhere.fr", publicKey, privateKey);
            var webPushClient = new WebPushClient();
            string message = "Scrapper has stopped working. Please check the server";
            var payload = JsonSerializer.Serialize(new
            {
                message,
                url = "",
            });
            await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
            Console.WriteLine($"Notification sent to subscription with Id {subscription.NotificationSubscriptionId}");
        }
    }
}
