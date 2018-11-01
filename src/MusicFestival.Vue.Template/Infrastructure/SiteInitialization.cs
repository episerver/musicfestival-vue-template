using EPiServer.ContentApi.Core.Configuration;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Search;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using MusicFestival.Template.Infrastructure.WebApi;
using MusicFestival.Template.Models;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MusicFestival.Template.Infrastructure
{
    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class SiteInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.Configure<ContentApiConfiguration>(config =>
            {
                config.Default()
                    .SetMinimumRoles(string.Empty)
                    .SetRequiredRole("Content Api Access")
                    .SetMultiSiteFilteringEnabled(true)
                    .SetSiteDefinitionApiEnabled(true);
            });

            // Register the extended content model mapper to be able to provide custom models from content api
            context.Services.Intercept<IContentModelMapper>((locator, defaultModelMapper) =>
                new ExtendedContentModelMapper(
                    locator.GetInstance<IUrlResolver>(),
                    defaultModelMapper,
                    locator.GetInstance<ServiceAccessor<HttpContextBase>>(),
                    locator.GetInstance<IContentVersionRepository>()
                    )
            );

            context.Services.Configure<ContentApiSearchConfiguration>(config =>
            {
                config.Default().SetSearchCacheDuration(TimeSpan.Zero);
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.StructureMap()));

            GlobalConfiguration.Configure(config =>
            {
                config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
                config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
                config.Formatters.XmlFormatter.UseXmlSerializer = true;
                config.DependencyResolver = new StructureMapResolver(context.StructureMap());
                config.MapHttpAttributeRoutes();
                config.EnableCors();
            });
            context.Services.AddTransient<IPropertyModelConverter, BuyTicketBlockPropertyModelHandler>();
        }

        public void Initialize(InitializationEngine context)
        {
            var options = ServiceLocator.Current.GetInstance<DisplayOptions>();
            options
                .Add("full", "Full", ContentAreaTags.FullWidth, "", "epi-icon__layout--full")
                .Add("wide", "Wide", ContentAreaTags.TwoThirdsWidth, "", "epi-icon__layout--two-thirds")
                .Add("half", "Half", ContentAreaTags.HalfWidth, "", "epi-icon__layout--half")
                .Add("narrow", "Narrow", ContentAreaTags.OneThirdWidth, "", "epi-icon__layout--one-third");

            AreaRegistration.RegisterAllAreas();
        }

        public void Uninitialize(InitializationEngine context) { }

        public static class ContentAreaTags
        {
            public const string FullWidth = "u-md-sizeFull";
            public const string TwoThirdsWidth = "u-md-size2of3";
            public const string HalfWidth = "u-md-size1of2";
            public const string OneThirdWidth = "u-md-size1of3";
            public const string NoRenderer = "norenderer";
        }
    }
}