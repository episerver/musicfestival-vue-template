using EPiServer.ContentApi.Routing;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using EPiServer.Web.Routing.Segments;
using System.Web;

namespace MusicFestival.Template.Infrastructure.Routing
{
    /// <summary>
    /// Custom routing event handler for Music Festival.    
    /// </summary>
    public class CustomContentApiRoutingEventHandler : RoutingEventHandler
    {
        public CustomContentApiRoutingEventHandler(
            IContentRouteEvents routeEvents,
            ServiceAccessor<HttpContextBase> httpContextAccessor,
            ContentApiRouteService contentApiRouteService)
            : base(routeEvents, httpContextAccessor, contentApiRouteService)
        {            
        }

        // If language does not exists in the routing context, we return ContentLanguage.PreferredCulture as default language
        protected override string GetLanguage(SegmentContext routingContext, HttpRequestBase request)
        {
            var language = routingContext.Language ?? routingContext.ContentLanguage ?? ContentLanguage.PreferredCulture.Name;
            
            return language;
        }       
    }
}