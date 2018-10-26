using EPiServer.Core;

namespace MusicFestival.Template.Models.Preview
{
    public class PreviewBlock : PageData
    {
        public IContent PreviewContent { get; }

        public PreviewBlock(PageData currentPage, IContent previewContent)
            : base(currentPage)
        {
            PreviewContent = previewContent;
        }
    }
}