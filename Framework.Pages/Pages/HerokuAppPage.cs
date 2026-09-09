using Microsoft.Playwright;
using Serilog;
using System.Xml.Linq;

namespace DCB.Pages
{
    public class HerokuAppPage
    {
        private readonly IPage _page;

        public HerokuAppPage(IPage page)
        {
            _page = page;
        }
        private ILocator CheckBoxesLink1 => _page.Locator("a[href='/checkboxes']");

        private ILocator CheckBoxesLink2 => _page.GetByRole(AriaRole.Link, new() { Name = "checkboxes" });

        private ILocator CheckBoxes => _page.Locator("input[type='checkbox']");

        private ILocator Gallery => _page.GetByText("Gallery");

        private ILocator DisappearingElements => _page.GetByText("Disappearing Elements");

        private ILocator DragDropLink => _page.GetByText("Drag and Drop");

        private ILocator DragSource => _page.Locator("#column-a");

        //div[@id='column-a']
        private ILocator DropDestination => _page.Locator("#column-b");



        public async Task CheckBoxNavigation1()
        {
            await CheckBoxesLink1.ClickAsync();
            Log.Debug("Checkbox 1 link clicked ");
        }

        public async Task CheckCheckBox1()
        {
            await CheckBoxes.Nth(0).CheckAsync();
            Log.Debug("Checkbox 1  clicked ");
        }

        public async Task CheckCheckBox2()
        {
            await CheckBoxes.Nth(1).UncheckAsync();
            Log.Debug("Checkbox 2  clicked ");
        }


        public async Task ClickGallery()
        {
            await DisappearingElements.ClickAsync();
            await Gallery.ClickAsync();

            Log.Debug("Disappearing Elements Clicked");

            Log.Debug("Gallery Clicked");
        }

        public async Task DragDrop()
        {
            await DragDropLink.ClickAsync();
            await DragSource.DragToAsync(DropDestination);
           
        }


    }
}