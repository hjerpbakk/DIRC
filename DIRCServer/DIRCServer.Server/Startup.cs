using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace DIRCServer.Server
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map(
                "/signalr", 
                map =>
                {
                    // Setup the CORS middleware to run before SignalR.
                    // By default this will allow all origins. You can 
                    // configure the set of origins and/or http verbs by
                    // providing a CORS options with a different policy.
                    // CORS = Cross Origin Resource Sharing
                    map.UseCors(CorsOptions.AllowAll);

                    var hubConfiguration = new HubConfiguration
                    {
                        // You can enable JSONP by uncommenting line below.
                        // JSONP requests are insecure but some older browsers (and some
                        // versions of IE) require JSONP to work cross domain
                        // EnableJSONP = true,
                        // EnableJavaScriptProxies = false
                    };

                    // Run the SignalR pipeline. We're not using MapSignalR
                    // since this branch already runs under the "/signalr"
                    // path.
                    map.RunSignalR(hubConfiguration);
                });
        }
    }
}