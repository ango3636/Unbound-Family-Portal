using ServiceStack.Auth;
using FamilyPortal.Data;

[assembly: HostingStartup(typeof(FamilyPortal.ConfigureAuth))]

namespace FamilyPortal;

public class ConfigureAuth : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            services.AddPlugin(new AuthFeature(IdentityAuth.For<ApplicationUser>(options => {
                options.SessionFactory = () => new CustomUserSession();
                options.CredentialsAuth();
                options.AdminUsersFeature();
            })));
        });
}