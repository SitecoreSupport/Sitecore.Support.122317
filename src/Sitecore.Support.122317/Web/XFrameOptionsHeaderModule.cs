using Sitecore.Diagnostics;
using System;
using System.Linq;
using System.Web;

namespace Sitecore.Support.Web
{
  internal class XFrameOptionsHeaderModule : IHttpModule
  {
    protected HttpApplication Application;
    private const string X_Frame_Options = "X-Frame-Options";

    private void BeginRequestHandler(object sender, EventArgs e)
    {
      Assert.ArgumentNotNull(sender, "sender");
      Assert.ArgumentNotNull(e, "e");
      if (HttpContext.Current.Request.RawUrl.StartsWith("/sitecore/shell"))
      {
        HttpResponse response = HttpContext.Current.Response;
        if (response.Headers.AllKeys.All<string>(key => (key != X_Frame_Options)) && (response.ContentType == "text/html"))
        {
          response.Headers.Add(X_Frame_Options, "SAMEORIGIN");
        }
      }
    }

    public virtual void Init(HttpApplication context)
    {
      Assert.ArgumentNotNull(context, "context");
      this.Application = context;
      this.Application.BeginRequest += new EventHandler(this.BeginRequestHandler);
    }
    public virtual void Dispose()
    {
    }
  }
}