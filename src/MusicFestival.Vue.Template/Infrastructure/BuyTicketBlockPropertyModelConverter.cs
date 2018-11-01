using System.Collections.Generic;
using System.Globalization;
using MusicFestival.Template.Models.Blocks;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.SpecializedProperties;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;

namespace MusicFestival.Template.Infrastructure
{
    /// <summary>
    /// Used to support conversion of <see cref="MusicFestival.Template.Infrastructure.BuyTicketBlockPropertyModel"/>
    /// which is returned from API as a property of LandingPage
    /// </summary>
    public class BuyTicketBlockPropertyModelConverter: IPropertyModelConverter
    {
        /// <summary>
        /// Verifies that the instance of <see cref="IPropertyModelConverter"/> has the correct <see cref="IPropertyModel"/> registered for the provided PropertyData type.
        /// </summary>
        /// <param name="propertyData">An instance of PropertyData to check</param>
        /// <returns></returns>
        public bool HasPropertyModelAssociatedWith(PropertyData propertyData)
        {
            var blockPropertyData = propertyData as PropertyBlock;

            if (!(blockPropertyData?.Value is BuyTicketBlock))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converts an instance of <see cref="PropertyData"/> into a specific Property Model
        /// </summary>
        /// <param name="propertyData">An instance of PropertyData</param>
        public IPropertyModel ConvertToPropertyModel(PropertyData propertyData, CultureInfo language, bool excludePersonalizedContent, bool expand)
        {
            return new BuyTicketBlockPropertyModel((PropertyBlock) propertyData);
        }

        public int SortOrder => 1000;

        public IEnumerable<TypeModel> ModelTypes => new List<TypeModel>
        {
            new TypeModel()
            {
                ModelType = typeof(BlockPropertyDefinitionType),
                ModelTypeString = nameof(BlockPropertyDefinitionType),
                PropertyType = typeof(BuyTicketBlock)
            }
        };
    }
}