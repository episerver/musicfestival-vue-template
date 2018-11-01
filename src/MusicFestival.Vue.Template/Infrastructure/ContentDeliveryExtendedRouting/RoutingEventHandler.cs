using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using System;
using System.Linq;
using System.Web;

namespace ContentDeliveryExtendedRouting.Routing
{
    [ServiceConfiguration(IncludeServiceAccessor = false)]
    public class RoutingEventHandler : IDisposable
    {
        private readonly ServiceAccessor<HttpContextBase> _httpContextAccessor;
        private readonly IContentRouteEvents _contentRouteEvents;

        public RoutingEventHandler(IContentRouteEvents routeEvents, ServiceAccessor<HttpContextBase> httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _contentRouteEvents = routeEvents;
            _contentRouteEvents.RoutedContent += RoutedContent;
        }

        private void RoutedContent(object sender, RoutingEventArgs e)
        {
            var httpContext = _httpContextAccessor();
            var routingContext = e.RoutingSegmentContext;
            var request = httpContext?.Request;
            if (request != null && request.AcceptTypes.Contains(RoutingConstants.JsonContentType))
            {
                //make sure routed language is first in accept language header
                var language = routingContext.Language ?? routingContext.ContentLanguage ?? ContentLanguage.PreferredCulture.Name;
                var acceptLanguageHeader = request.Headers[RoutingConstants.AcceptLanguage];
                request.Headers[RoutingConstants.AcceptLanguage] = string.IsNullOrEmpty(acceptLanguageHeader) ?
                    language :
                    $"{language}, { acceptLanguageHeader}";

                var property = routingContext.GetCustomRouteData<string>(RoutingConstants.RoutedPropertyKey);
                var shouldGetChildren = routingContext.GetCustomRouteData<string>(RoutingConstants.ChildrenKey);
                var contentApiChildPath = $"/{EPiServer.ContentApi.Core.Internal.RouteConstants.BaseContentApiRoute}content/{routingContext.RoutedContentLink}/children";

                if (bool.TryParse(shouldGetChildren, out bool result) && result)
                {
                    httpContext.RewritePath(contentApiChildPath);
                }
                else
                {
                    httpContext.RewritePath(property != null ?
                        $"/{EPiServer.ContentApi.Core.Internal.RouteConstants.BaseContentApiRoute}content/{routingContext.RoutedContentLink}?{RoutingConstants.RoutedPropertyKey}={property}" :
                        $"/{EPiServer.ContentApi.Core.Internal.RouteConstants.BaseContentApiRoute}content/{routingContext.RoutedContentLink}");
                }


                //Set RouteData to null to pass the request to next routes (WebApi route)
                e.RoutingSegmentContext.RouteData = null;
            }
        }

        public void Dispose()
        {
            if (_contentRouteEvents != null)
                _contentRouteEvents.RoutedContent -= RoutedContent;
        }
    }
}