using Owin;

namespace WebSocketSpike.WebServer
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseFileServer(enableDirectoryBrowsing: true);
        }
    }
}