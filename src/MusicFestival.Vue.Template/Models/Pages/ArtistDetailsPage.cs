using System;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;
using MusicFestival.Template.Models.Pages;
using MusicFestival.Template.Models;
using EPiServer;

namespace MusicFestival.Template.Content.Pages
{
    /// <summary>
    /// Model for Assets/Scripts/components/pages/ArtistDetailsPage.vue
    /// </summary>
    [ContentType(DisplayName = "Artist Page", GUID = "9e98d26a-6f06-4c35-bfbd-8b850a0fa433", Description = "Page with details about an artist")]
    [SiteImageUrl]
    [AvailableContentTypes(Availability.None)]
    public class ArtistDetailsPage : BasePage
    {
        [Display(
            Name = "Artist Name",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string ArtistName { get; set; }

        [Display(
            Name = "Artist Photo",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [UIHint(UIHint.Image)]
        public virtual Url ArtistPhoto { get; set; }

        [CultureSpecific]
        [Display(
                    Name = "Artist Description",
                    Description = "Description to appear on the artist detail page.",
                    GroupName = SystemTabNames.Content,
                    Order = 30)]
        public virtual XhtmlString ArtistDescription { get; set; }

        [Display(
            Name = "Artist Genre",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        public virtual string ArtistGenre { get; set; }

        [Display(
            Name = "Performance Start Time",
            GroupName = SystemTabNames.Content,
            Order = 50)]
        public virtual DateTime PerformanceStartTime { get; set; }

        [Display(
            Name = "Performance End Time",
            GroupName = SystemTabNames.Content,
            Order = 55)]
        public virtual DateTime PerformanceEndTime { get; set; }

        [Display(
            Name = "Stage Name",
            GroupName = SystemTabNames.Content,
            Order = 60)]
        public virtual string StageName { get; set; }

        [Display(
            Name = "Headliner",
            GroupName = SystemTabNames.Content,
            Order = 70)]
        public virtual bool ArtistIsHeadliner { get; set; }
    }
}