using EPiServer.Cms.Shell;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.Editor;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using EPiServer.Web.Routing;
using MusicFestival.Template.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicFestival.Template.Models
{
    /// <summary>
    /// A decorator for the DefaultContentModelMapper. We need this to extend the returned models with custom properties
    /// like languages or parentUrl.
    /// </summary>
    public class ExtendedContentModelMapper : IContentModelMapper
    {
        private readonly IContentModelMapper _defaultContentModelMapper;
        private readonly IUrlResolver _urlResolver;
        private readonly ServiceAccessor<HttpContextBase> _httpContextAccessor;
        private readonly IContentVersionRepository _versionRepository;

        public ExtendedContentModelMapper(IUrlResolver urlResolver, IContentModelMapper defaultContentModelMapper, ServiceAccessor<HttpContextBase> httpContextAccessor, IContentVersionRepository versionRepository)
        {
            _urlResolver = urlResolver;
            _defaultContentModelMapper = defaultContentModelMapper;
            _httpContextAccessor = httpContextAccessor;
            _versionRepository = versionRepository;
        }

        public IEnumerable<IPropertyModelConverter> PropertyModelConverters { get; }

        /// <summary>
        /// Maps an instance of IContent to ContentApiModel and additionally add info about existing languages
        /// </summary>
        public ContentApiModel TransformContent(IContent content, bool excludePersonalizedContent, string expand)
        {
            var contentModel = _defaultContentModelMapper.TransformContent(content, excludePersonalizedContent, expand);
            contentModel.Url = ResolveUrl(content.ContentLink, content.LanguageBranch());
            contentModel.Properties = contentModel.Properties.Select(FlattenProperty).ToDictionary(x=>x.Key, x=>x.Value);

            var parentUrl = ResolveUrl(content.ParentLink, content.LanguageBranch());
            contentModel.Properties.Add("parentUrl", parentUrl);

            if (contentModel.ExistingLanguages != null)
            {
                ExtendLanguageModels(contentModel, content);
            }

            return contentModel;
        }

        /// <summary>
        /// Extends the language models with a link to each language.
        /// The links are used in Assets/Scripts/components/LanguageSelector.vue
        /// </summary>
        private void ExtendLanguageModels(ContentApiModel contentModel, IContent content)
        {
            var listPublished = _versionRepository.ListPublished(content.ContentLink);
            var listLanguagesWithPublishedContents = listPublished.Select(x => x.LanguageBranch);

            // Filter the existingLanguages list to make it contain only languages which has a published version
            contentModel.ExistingLanguages =
                contentModel.ExistingLanguages.Where(x => listLanguagesWithPublishedContents.Contains(x.Name)).Select(x => new ExtendedLanguageModel(x)
                {
                    Link = ResolveUrl(content.ContentLink, x.Name)
                }).ToList<LanguageModel>();
        }

        private class ExtendedLanguageModel: LanguageModel
        {
            public ExtendedLanguageModel(LanguageModel item)
            {
                DisplayName = item.DisplayName;
                Name = item.Name;
            }

            public string Link { get; set; }
        }

        private static KeyValuePair<string, object> FlattenProperty(KeyValuePair<string, object> property)
        {
            return new KeyValuePair<string, object>(property.Key, FlattenPropertyValue(property.Value));
        }

        /// <summary>
        /// Extract the actual value from the property model.
        /// In our simple Vue app we are only interested about the property values,
        /// flattening the property model will simplify our client side components.
        /// </summary>
        private static object FlattenPropertyValue(object propertyValue)
        {
            switch (propertyValue)
            {
                case ContentAreaPropertyModel propertyModel:
                    return propertyModel?.ExpandedValue?.Select(x=>ConvertContentAreaItem(x, propertyModel));
                case BuyTicketBlockPropertyModel propertyModel:
                    return propertyModel.Value;
                case PropertyModel<string, PropertyString> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<string, PropertyUrl> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<DateTime?, PropertyDate> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<bool?, PropertyBoolean> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<string, PropertyLongString> propertyModel:
                    return propertyModel.Value;
                case PropertyModel<string, PropertyXhtmlString> propertyModel:
                    return propertyModel.Value;
                case CategoryPropertyModel propertyModel:
                    return propertyModel.Value;
                default:
                    return propertyValue;
            }
        }

        /// <summary>
        /// We need to extend the model for content areas with available display options so our component will get a correct css class
        /// see how it's used in Assets/Scripts/components/ContentArea.vue
        /// </summary>
        private static object ConvertContentAreaItem(ContentApiModel contentApiModel, ContentAreaPropertyModel propertyModel)
        {
            var contentModelDisplayOption = propertyModel.Value.FirstOrDefault(x => x.ContentLink.Id == contentApiModel.ContentLink.Id)?.DisplayOption;
            contentApiModel.Properties.Add("displayOption", contentModelDisplayOption);
            return contentApiModel;
        }

        private string ResolveUrl(ContentReference contentLink, string language)
        {
            var contextMode = GetContextMode();
            return _urlResolver.GetUrl(contentLink, language, new UrlResolverArguments
            {
                ContextMode = contextMode
            });
        }

        /// <summary>
        /// The "epieditmode" querystring parameter is added to URLs by Episerver as a way to keep track of what context is currently active.
        /// If there is no "epieditmode" parameter we're in regular view mode.
        /// If the "epieditmode" parameter has value "True" we're in edit mode.
        /// If the "epieditmode" parameter has value "False" we're in preview mode.
        /// All of these different modes will resolve to different URLs for the same content.
        /// </summary>
        private ContextMode GetContextMode()
        {
            var httpCtx = _httpContextAccessor();
            if (httpCtx == null || httpCtx.Request == null || httpCtx.Request.QueryString[PageEditing.EpiEditMode] == null)
            {
                return ContextMode.Default;
            }
            if (bool.TryParse(httpCtx.Request.QueryString[PageEditing.EpiEditMode], out bool editMode))
            {
                return editMode ? ContextMode.Edit : ContextMode.Preview;
            }
            return ContextMode.Undefined;
        }
    }
}