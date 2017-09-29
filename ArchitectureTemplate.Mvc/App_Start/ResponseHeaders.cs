using System;
using System.Web;

namespace ArchitectureTemplate.Mvc
{
    public class ResponseHeadersModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }
 
        public void Dispose() { }
 
        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Set("Server", "Ops não sei!");
        }
    }
}