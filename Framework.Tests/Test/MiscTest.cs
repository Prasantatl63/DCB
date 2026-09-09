using Microsoft.Playwright;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;
//using DCB.Framework.Core;
using NUnit.Framework;

namespace DCB.Tests
{
   [TestFixture]
   [AllureNUnit]
   
    public class MiscTest
    {
        IPlaywright playwright1;
        IBrowser browser1;
        IBrowserContext context1;
        IPage page1;


        [SetUp]
        public void Setup()
        {
        }
         //[Test]
         [AllureTag("smoke")]
         [AllureFeature("Login")]
         [AllureStory("Valid Login")]
         [AllureAfter]
         [AllureSeverity(SeverityLevel.critical)]
       public async Task StartPlayWright()
        {
            // Initialize Playwright
            using var playwright = await Playwright.CreateAsync();

            // Launch a browser (Chromium)cls
            
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true // set false if you want to see the browser
            });

        var contexta = browser.NewContextAsync();

          var contextb = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
            UserAgent = "MyCustomUserAgent"
        });

        // Create a new page inside the context
        var page = await contextb.NewPageAsync();

            // Create a new page
            //var page = await browser.NewPageAsync();

            // Navigate to Playwright website
            await page.GotoAsync("https://timesofindia.indiatimes.com/");

            //await page.Locator("div.Kt6Pm.style_change.T5Q6J")
              //      .Filter(new() { HasText = "Pradhan meets LS Speaker Birla" })
                //    .ClickAsync();


            //await page.GetByText("Pradhan meets LS Speaker Birla").ClickAsync();

            //System.Threading.Thread.Sleep(5000);

            //var getStarted1 = page.GetByText("Get started");
          
            //await getStarted1.ClickAsync();

            //await getStarted.ClickAsync();


        }
  
         public async Task LocatorTest()
        {
            // Initialize Playwright
            using var playwright = await Playwright.CreateAsync();

            // Launch a browser (Chromium)
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true // set false if you want to see the browser
            });
      
            // Create a new page
            var page = await browser.NewPageAsync();

            // Navigate to Playwright website
            await page.GotoAsync("https://timesofindia.indiatimes.com/");

            await page.Locator("div.Kt6Pm.style_change.T5Q6J")
                    .Filter(new() { HasText = "Pradhan meets LS Speaker Birla" })
                    .ClickAsync();         


        }
  
        [Test]
        [Category("Shard1")]
        [AllureTag("smoke")]
        [AllureSeverity(SeverityLevel.critical)]
        public async Task LocatorAriaRole1()
        {
            // Initialize Playwright
            using var playwright = await Playwright.CreateAsync();

            // Launch a browser (Chromium)
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true ,
                // set false if you want to see the browser
            });
            
            // Create a new page
            var page = await browser.NewPageAsync();

            // Navigate to Playwright website
            await page.GotoAsync("https://github.com/w3c/aria-practices/issues/1132");

           var issueIcon = page.GetByRole(AriaRole.Img, new() { Name = "Issue" });

            var issueIcon1 = page.GetByRole(AriaRole.Img, new() { Name = "Issue" });

            await issueIcon.ClickAsync();
            var newIssueButton = await page.QuerySelectorAsync("span[data-component='text']");

            if (newIssueButton != null)
            {
                //await newIssueButton.ClickAsync();
            }
            //await  newIssueButton.ClickAsync();

        //await page.GoBackAsync();
        }

          [Test]
          [AllureTag("smoke")]
          [AllureSeverity(SeverityLevel.critical)]
          public async Task LocatorAriaRole2()
        {
            // Initialize Playwright
            using var playwright = await Playwright.CreateAsync();

            // Launch a browser (Chromium)
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true ,
            });

            var context = await browser.NewContextAsync();

            // Create a new page
            var page = await context.NewPageAsync();

            // Navigate to Playwright website
            await page.GotoAsync("https://www.w3schools.com/html/html_forms.asp");    

            // Locator by role and accessible name

            var submitButton = page.Locator("form").GetByRole(AriaRole.Button, new() { Name = "Submit" }).First;

            var count = submitButton.CountAsync();
            await submitButton.ClickAsync();
   


        }

        [Test]
        [Category("Shard2")]
        [AllureTag("smoke")]
        // [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("QA Team")]
           public async Task LocatorAriaRole()
        {
                using var playwright = await Playwright.CreateAsync();

                // Launch a browser (Chromium)
                await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                 {
                       Headless = true ,
                });

                var context = await browser.NewContextAsync();

                // Create a new page
                var page = await context.NewPageAsync();
           
           try
            {
                 // Initialize Playwright
                
                // Navigate to Playwright website
                await page.GotoAsync("https://www.w3schools.com/css/css_dropdowns.asp");    

                // Locator by role and accessible name

                //<button class="dropbtn">Dropdown Menu</button>

                //await page.Locator(".dropbtn").First.ClickAsync();
                //await page.GetByText("Link 1").ClickAsync();
            
                //await page.SelectOptionAsync(".dropbtn","Link 1");



                await page.ScreenshotAsync(new()
                {
                     Path = "screenshot.png",
                    FullPage = true
                }
                );

                AllureApi.AddAttachment(
                 "Screenshot",
                 "image/png",
                 "screenshot.png");


                  }
            catch(Exception ex)
            {
                 throw;
            }
   


        }

        [Test]
        [Category("Shard3")]
        [AllureTag("smoke")]
       //[AllureSeverity(SeverityLevel.critical)]
            [AllureOwner("QA Team")]
           public async Task Controls()
        {
            // Initialize Playwright
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true , 
            });

            var context = await browser.NewContextAsync();
            // Create a new page
            var page = await browser.NewPageAsync();

            var url = "https://www.w3schools.com/html/html_forms.asp";

            // Navigate to Playwright website
            await page.GotoAsync(url);    
            //check box check
            //var checkbox = page.Locator("#chechk1").CheckAsync();
            //check box uncheck
           // var checkbox1 = page.Locator("#chechk1").UncheckAsync();
            // read and check input value 
           // var res = page.Locator("#name").InputValueAsync();                  
            var iframe = page.FrameLocator("#iframeResult");


            var iframe1 = page.Locator("").ContentFrame.GetByText("");

            //var button = iframe.GetByRole(AriaRole.Button, new() { Name = "Click me" });
           // var greenbutton = iframe.Locator("button.testbtn");
            //await greenbutton.ClickAsync();
            //await button.ClickAsync();
            var textbox1 = page.GetByRole(AriaRole.Textbox,new(){Name ="First name:"});
            var textbox2 = page.GetByRole(AriaRole.Textbox,new(){Name ="Last name:"});
            await textbox1.FillAsync("Prasanta"); 
            await textbox2.FillAsync("Mishra"); 

             var fname = textbox1.InputValueAsync;
             var lname = textbox2.InputValueAsync;

             await Assertions.Expect(textbox1).ToHaveValueAsync("Prasanta");
             await Assertions.Expect(textbox2).ToHaveValueAsync("Mishra");
   
            // 13. Playwright automatically handles Shadow DOM 
            // 14 Handling Iframes 
            //      var iframe = page.FrameLocator("#iframeResult");
            //      ContentFrameAsync() Gets the frame object

        }
        [Test]
        public async Task NewTest()
        {
            try
            {
                var playwright = await Playwright.CreateAsync();

                var browser = await playwright.Chromium.LaunchAsync(
                 new BrowserTypeLaunchOptions { 
                 Headless =false}
                    );
                var page2 = await browser.NewPageAsync();
                var context = await browser.NewContextAsync();
                var page = await context.NewPageAsync();
                await page.GotoAsync("https://the-internet.herokuapp.com/");



                await page.GetByText("Add/Remove Elements").ClickAsync();

                await page.GetByRole(AriaRole.Button, new() { Name = "Add Element" }).ClickAsync();

                await page.GetByRole(AriaRole.Button, new() { Name = "Delete" }).ClickAsync();

                await page.GoBackAsync();
                //await page.Locator("a[href ='/basic_auth']").First.ClickAsync();
                await page.Locator("a[href ='/basic_auth']").ClickAsync();

                var context1 = await browser.NewContextAsync(
                 new BrowserNewContextOptions
                 {
                     HttpCredentials = new HttpCredentials
                     {
                         Username = "admin",
                         Password = "admin"
                     }
                 });

                var page1 = await context.NewPageAsync();

                await page.GotoAsync("https://the-internet.herokuapp.com/basic_auth");

                Console.WriteLine(await page1.TitleAsync());


                //Practice 
                await page2.Locator("#country").SelectOptionAsync("india");
                await page2.Locator("#country").SelectTextAsync();


                await context.StorageStateAsync(
                new BrowserContextStorageStateOptions
                {
                    Path = "auth.json"
                });


                

            }
            catch (Exception ex)
            {

            }
        }

        [Test]
        public async Task PracticeStorage()
        {
            //Set Up 

            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(
             new BrowserTypeLaunchOptions
             {
                 Headless = false
             }
                );
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://practicetestautomation.com/practice-test-login/");

            await page.GetByLabel("Username").FillAsync("student");
            await page.GetByLabel("Password").FillAsync("Password123");
            await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();



            // Get the storage state
            var storageState = await context.StorageStateAsync();



            var context2 = await browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                StorageState = storageState
            });

            var page2 = await context2.NewPageAsync();
            await page2.GotoAsync("https://practicetestautomation.com/practice-test-login/");

        }

        public async Task SetUp()
        {
            playwright1 = await Playwright.CreateAsync();
            browser1 = await playwright1.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions { Headless = false }
                );

            context1 = await browser1.NewContextAsync();
            page1 = await context1.NewPageAsync();
        }

        [Test]
        public async Task TabTest()
        {
            await SetUp();
            await page1.GotoAsync("https://the-internet.herokuapp.com/");
            var page2 = await context1.NewPageAsync();
            var page3 = await context1.NewPageAsync();
            var page4 = await context1.NewPageAsync();
            await page2.GotoAsync("https://the-internet.herokuapp.com/challenging_dom");
            await page1.BringToFrontAsync();
            await page3.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
            await page4.GotoAsync("https://the-internet.herokuapp.com/challenging_dom");
            
            await context1.Pages.First(p => p.Url.Contains("checkboxes")).BringToFrontAsync();
        }

        [Test]
        public async Task ContextMenu()
        {
            await SetUp();
            await page1.GotoAsync("https://the-internet.herokuapp.com/dropdown");
            
            //context menu
            //await page1.Locator("div[id='hot-spot']").ClickAsync(new() { Button = MouseButton.Right });
            //await Task.Delay(10000);

            //await page1.Locator("#dropdown").SelectOptionAsync("1");
            await page1.Locator("#dropdown").SelectOptionAsync(new SelectOptionValue
            {
                Label = "Option 1"
            });

            await page1.Locator("#dropdown").SelectOptionAsync("2");
            await Assertions.Expect(page1.GetByText("Login successful")).ToBeVisibleAsync();


            await page1.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            await context1.Tracing.StartAsync(new()
            { 
            Screenshots = true,
            Snapshots = true,
            Sources = true 
            }
                  
                );

        }

        [Test]
        public async Task InitialSetUp()
        {
            await SetUp();

            await page1.GotoAsync("https://the-internet.herokuapp.com/javascript_alerts");
            await Task.Delay(10000);
            page1.Dialog += async (_, dialog) =>
            {
                ////Console.WriteLine("Dialog Type: " + dialog.Type);
                //Console.WriteLine("Dialog Message: " + dialog.Message);
                //await Task.Delay(10000);
                await dialog.AcceptAsync();
            };

            await page1.GetByRole(
                AriaRole.Button,
                new() { Name = "Click for JS Alert" }
            ).ClickAsync();
            

        }


    }
}
        

    


