using Microsoft.Owin;
using Owin;
using SignalR_StackOverflow;

[assembly: OwinStartup(typeof(SignalR_StackOverflow.Startup))]
namespace SignalR_StackOverflow
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}