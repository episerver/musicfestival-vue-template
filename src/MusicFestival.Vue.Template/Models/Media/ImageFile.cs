using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace MusicFestival.Template.Models.Media
{
    /// <summary>
    /// Model for Assets/Scripts/components/media/ImageFile.vue
    /// </summary>
    [ContentType(DisplayName = "Image File", GUID = "a736bc13-d17c-46e2-ad5d-e37bd3af086b", AvailableInEditMode = false)]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png")]
    public class ImageFile : ImageData
    {
    }
}