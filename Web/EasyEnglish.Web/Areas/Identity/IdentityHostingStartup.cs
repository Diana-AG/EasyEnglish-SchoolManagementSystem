﻿using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EasyEnglish.Web.Areas.Identity.IdentityHostingStartup))]

namespace EasyEnglish.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
