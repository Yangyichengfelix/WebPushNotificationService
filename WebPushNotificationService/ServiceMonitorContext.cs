
namespace WebPushNotificationService
{
    public class ServiceMonitorContext : DbContext
    {
        public ServiceMonitorContext(DbContextOptions<ServiceMonitorContext> options) : base(options)
        {

        }

        public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }
    }
}
