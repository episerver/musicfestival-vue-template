using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EPiServer.Web.Routing.Segments;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace ContentDeliveryExtendedRouting.Routing
{
    [ServiceConfiguration(IncludeServiceAccessor = false)]
    public class PropertyPartialRouter : IPartialRouter<IContent, IContent>
    {
        private readonly ServiceAccessor<HttpContextBase> _httpContextAccessor;
        private readonly PropertyResolver _propertyResolver;
        private ConcurrentDictionary<Type, ConcurrentDictionary<string, bool>> _propertyCache = new ConcurrentDictionary<Type, ConcurrentDictionary<string, bool>>();
        private IEnumerable<Type> _metadataInterfaces = new[] {
                typeof(IContent),
                typeof(ILocale),
                typeof(ILocalizable),
                typeof(IModifiedTrackable),
                typeof(IVersionable),
                typeof(IResourceable),
                typeof(IChangeTrackable),
                typeof(IRoutable),
                typeof(ICategorizable),
                typeof(IBinaryStorable),
                typeof(IContentAsset),
                typeof(IContentMedia)
            };
        private BindingFlags _caseInSensitiveFlags = BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public;

        public PropertyPartialRouter(ServiceAccessor<HttpContextBase> httpContextAccessor, PropertyResolver propertyResolver)
        {
            _httpContextAccessor = httpContextAccessor;
            _propertyResolver = propertyResolver;
            PropertyDefinitionRepository.PropertyDefinitionDeleted += ClearPropertyLookup;
            PropertyDefinitionRepository.PropertyDefinitionSaved += ClearPropertyLookup;
        }

        public object RoutePartial(IContent content, SegmentContext segmentContext)
        {
            var context = _httpContextAccessor();
            var request = context?.Request;
            if (request != null && request.AcceptTypes.Contains(RoutingConstants.JsonContentType))
            {
                var nextSegment = segmentContext.GetNextValue(segmentContext.RemainingPath);
                if (PropertyExist(content, nextSegment.Next))
                {
                    segmentContext.SetCustomRouteData(RoutingConstants.RoutedPropertyKey, nextSegment.Next);
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

        private bool PropertyExist(IContentData content, string propertyName)
        {
            var resolvedPropertiesOnType = _propertyCache.GetOrAdd(content.GetType(), t => new ConcurrentDictionary<string, bool>(StringComparer.OrdinalIgnoreCase));
            return resolvedPropertiesOnType.GetOrAdd(propertyName, prop => ResolveProperty(content, prop));
        }

        private bool ResolveProperty(IContentData content, string propertyName)
        {
            //First try to find property on typed model
            var property = content.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property != null && property.CanRead && property.CanWrite)
                return true;

            //Then check if property is in PropertyCollection
            var propertyData = content.Property[propertyName];
            if (propertyData != null)
                return true;

            //Finally check if it is defined on an interface
            return GetInterfaceForPropertyName(propertyName);
        }

        internal bool GetInterfaceForPropertyName(string propertyName)
        {
            return _metadataInterfaces.Any(i => i.GetProperty(propertyName, _caseInSensitiveFlags) != null);
        }

        private void ClearPropertyLookup(object sender, EventArgs e)
        {
            _propertyCache.Clear();
        }
    }
}