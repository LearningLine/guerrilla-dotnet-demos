using Owin;

namespace Chatty.Web
{
  public partial class Startup 
  {
      public void ConfigureSignalR(IAppBuilder appBuilder)
      {
          appBuilder.MapSignalR();
      }
  }
}