using System.Web;

namespace Herramienta.WebHost
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			var appHost = new AppHost();
			appHost.Init();
		}
	}
}
