using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EPiServer.Web.Routing.Segments;
using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace ContentDeliveryExtendedRouting.Routing
{
    [ServiceConfiguration(IncludeServiceAccessor = false)]
    public class ChildrenPartialRouter : IPartialRouter<IContent, IContent>
    {
        private readonly ServiceAccessor<HttpContextBase> _httpContextAccessor;

        public ChildrenPartialRouter(ServiceAccessor<HttpContextBase> httpContextAccessor, PropertyResolver propertyResolver)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public object RoutePartial(IContent content, SegmentContext segmentContext)
        {
            var context = _httpContextAccessor();
            var request = context?.Request;
            if (request != null && request.AcceptTypes.Contains(RoutingConstants.JsonContentType))
            {
                var nextSegment = segmentContext.GetNextValue(segmentContext.RemainingPath);
                if (IsChildren(nextSegment.Next))
                {
                    segmentContext.SetCustomRouteData(RoutingConstants.ChildrenKey, bool.TrueString);
                    segmentContext.RemainingPath = nextSegment.Remaining;
                    return content;
                }
            }
            return null;
        }

        public PartialRouteData GetPartialVirtualPath(IContent content, string language, RouteValueDictionary routeValues, RequestContext requestContext)
        {
            return null;
        }

        private bool IsChildren(string nextSegment)
        {
            return string.Equals("children", nextSegment, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}