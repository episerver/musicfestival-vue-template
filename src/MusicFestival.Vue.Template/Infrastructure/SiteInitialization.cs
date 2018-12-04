using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using System.Web.Mvc;
using System.Web;
using EPiServer.Web;
using MusicFestival.Template.Models;
using EPiServer.Web.Routing;
using EPiServer.Core;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Cms;
using EPiServer.ContentApi.Core.Configuration;
using EPiServer.ContentApi.Routing;
using MusicFestival.Template.Infrastructure.Routing;

namespace MusicFestival.Template.Infrastructure
{
    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization), typeof(ContentApiCmsInitialization))]
    public class SiteInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            // Register the extended content model mapper to be able to provide custom models from content api
            context.Services.Intercept<IContentModelMapper>((locator, defaultModelMapper) =>
                new ExtendedContentModelMapper(
                    locator.GetInstance<IUrlResolver>(),
                    defaultModelMapper,
                    locator.GetInstance<ServiceAccessor<HttpContextBase>>(),
                    locator.GetInstance<IContentVersionRepository>()
                    )
            );

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.StructureMap()));
            context.Services.AddTransient<IPropertyModelConverter, BuyTicketBlockPropertyModelConverter>();
            context.Services.AddTransient<RoutingEventHandler, CustomContentApiRoutingEventHandler>();

            // set minimumRoles to empty to allow anonymous calls (for visitors to view site in view mode)
            context.Services.Configure<ContentApiConfiguration>(config =>
            {
                config.Default().SetMinimumRoles(string.Empty);
            });
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