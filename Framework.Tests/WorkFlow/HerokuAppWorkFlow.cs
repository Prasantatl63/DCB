using DCB.Pages;
//using Framework.Utilities.Logging;
using Microsoft.Playwright;
using DCB.Framework.Configuration;
using DCB.Framework.Logging;

namespace DCB.Framework.Workflow
{
    public class HerokuAppWorkFlow 
    {
        private readonly IPage _page;
        private HerokuAppPage _herokuAppPage=null;


        [SetUp]
        public void InitializePages()
        {
            _herokuAppPage = new HerokuAppPage(_page);
        }

        public HerokuAppWorkFlow(IPage _page)
        {
            _herokuAppPage = new HerokuAppPage(_page);
        }

        public async Task NavigateToCheckBoxes()
        {
           await _herokuAppPage.CheckBoxNavigation1();
        }

        public async Task CheckCheckBoxes()
        {
           await _herokuAppPage.CheckCheckBox1();
           await _herokuAppPage.CheckCheckBox2();
        }

        public async Task NavigateAndClickGallery()
        {
            await _herokuAppPage.ClickGallery();
        }

        public async Task DragAndDrop()
        {

            await _herokuAppPage.DragDrop();
        }

         
    }
}