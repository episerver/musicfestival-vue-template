using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using System.Web.Mvc;
using System;
using EPiServer.ContentApi.Infrastructure;
using EPiServer.ContentApi.Search.Infrastructure;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web;
using EPiServer.ContentApi.Core;
using EPiServer.ContentApi.Core.Infrastructure;
using MusicFestival.Template.Infrastructure.WebApi;
using EPiServer.Web;
using MusicFestival.Template.Models;
using EPiServer.Web.Routing;
using EPiServer.Core;

namespace MusicFestival.Template.Infrastructure
{
    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class SiteInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {

            var contentApiOptions = new ContentApiOptions
            {
                MultiSiteFilteringEnabled = false
            };
            context.InitializeContentApi(contentApiOptions);

            // Register the extended content model mapper to be able to provide custom models from content api
            context.Services.Intercept<IContentModelMapper>((locator, defaultModelMapper) =>
                new ExtendedContentModelMapper(
                    locator.GetInstance<IUrlResolver>(),
                    defaultModelMapper,
                    locator.GetInstance<ServiceAccessor<HttpContextBase>>(),
                    locator.GetInstance<IContentVersionRepository>()
                    )
            );

            context.InitializeContentSearchApi(new ContentSearchApiOptions()
            {
                SearchCacheDuration = TimeSpan.Zero
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
            context.Services.AddTransient<IPropertyModelHandler, BuyTicketBlockPropertyModelHandler>();
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