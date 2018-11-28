using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace MusicFestival.Template.Models.Blocks
{
    /// <summary>
    /// Model for Assets/Scripts/components/blocks/BuyTicketBlock.vue
    /// </summary>
    [ContentType(DisplayName = "Buy Ticket Block", GUID = "ac096c4f-56ab-4396-9f5c-cfa923875c18", Description = "Allow visitors to buy a ticket",
        AvailableInEditMode = false)]
    [SiteImageUrl]
    public class BuyTicketBlock : BlockData
    {
        [CultureSpecific]
        [Required]
        public virtual string Heading { get; set; }

        [CultureSpecific]
        [Required]
        public virtual string Message { get; set; }
    }
}