using EPiServer.Core;

namespace MusicFestival.Template.Models.Preview
{
    public class BlockEditPageViewModel
    {
        public BlockEditPageViewModel(PageData page, IContent content)
        {
            PreviewBlock = new PreviewBlock(page, content);
        }

        public PreviewBlock PreviewBlock { get; }
    }
}