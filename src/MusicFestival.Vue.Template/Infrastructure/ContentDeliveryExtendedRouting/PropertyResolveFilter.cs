using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using EPiServer.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace ContentDeliveryExtendedRouting.Routing
{
    public class PropertyResolveFilter : IActionFilter
    {
        private readonly ServiceAccessor<HttpContextBase> _contextAccessor;

        public PropertyResolveFilter(ServiceAccessor<System.Web.HttpContextBase> contextAccesor)
        {
            _contextAccessor = contextAccesor;
        }
        public bool AllowMultiple => false;

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var currentProperty = _contextAccessor()?.Request.QueryString[RoutingConstants.RoutedPropertyKey] as string;
            var result = await continuation();
            if (result.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(currentProperty))
            {
                var jObject = JsonConvert.DeserializeObject<JObject>(await result.Content.ReadAsStringAsync());
                var property = jObject[currentProperty];

                var serializationSettings = new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                };

                jObject = new JObject();
                jObject.Add(currentProperty, property);
                var json = JsonConvert.SerializeObject(jObject, serializationSettings);
                result.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            return result;
        }
    }
}