using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BugTracker.Web.Areas.Identity.IdentityHostingStartup))]

namespace BugTracker.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
