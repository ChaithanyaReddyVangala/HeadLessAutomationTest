using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace HeadlessBrowserTalentTest
{
    [TestClass]
    public class HeadLessBrowser
    {
        [TestMethod]
        public void HeadlessChromeTest()
        {
            var chromeOptions = new ChromeOptions();

            chromeOptions.AddArguments("headless");   

            using (var driver = new ChromeDriver(chromeOptions))  //Chrome Browser instant
            {

                // 1. Maximize the browser
                driver.Manage().Window.Maximize();

                // 2. Go to the "url" provided by TalentTest
                driver.Navigate().GoToUrl("https://quiet-sands-70900.herokuapp.com/");

                // 3. Verify the user is in the SamMedia QA Talent Test page
                Assert.IsTrue(driver.Url.Contains("quiet-sands"), "user is not in the SamMedia QA Talent Test page");
                Console.WriteLine("User is Navigated to url");

                // 4. Find the Msisdn Locator( Web Element) in the page 
                var msisdnTextBox = driver.FindElementById("pinMsisdn");

                Thread.Sleep(5000);
                // Negative Scenario

                // 5. Enter the wrong msisdn number and click submit Msisdn.  
                msisdnTextBox.SendKeys("123456");
                Console.WriteLine("Entered incorrect msisdn");
                var submitMsisdbButton = driver.FindElementById("msisdnSubmit");
                submitMsisdbButton.Click();
                Thread.Sleep(5000);

                // 6. Verify Invalid Error Message Displayed
                var invalidErrorMessage = driver.FindElementByXPath("//*[@id='msisdnSection']/div[1]/div");
                Assert.IsTrue(invalidErrorMessage.Displayed, "Invalid Error Message Not Displayed ");


                Thread.Sleep(5000);

                msisdnTextBox.Clear();

                submitMsisdbButton.Click();
                Assert.IsTrue(invalidErrorMessage.Displayed, "Invalid Error Message Not Displayed ");
                Thread.Sleep(5000);

                //Positive Scenario

                msisdnTextBox.SendKeys("1111111");
                Console.WriteLine("Entered correct msisdn");
                Thread.Sleep(3000);
                submitMsisdbButton.Click();

                Thread.Sleep(5000);

                // 7. Verify the user is in Pin Page

                var pinTextBox = driver.FindElementById("pinPin");

                Assert.IsTrue(pinTextBox.Displayed, "User is not in "); //*[@id="pinSection"]/div

                var pinSubmitButton = driver.FindElementById("pinSubmit");

                pinSubmitButton.Click();
                var invalidErrorPinMessage = driver.FindElementByXPath("//*[@id='pinSection']/div");
                Assert.IsTrue(invalidErrorPinMessage.Displayed, "Invalid Error Pin Message Not Displayed ");
                Thread.Sleep(5000);

                pinTextBox.SendKeys("12345");
                Thread.Sleep(5000);
                Assert.IsTrue(invalidErrorPinMessage.Displayed, "Invalid Error Pin Message Not Displayed ");
                pinTextBox.Clear();

                pinTextBox.SendKeys("4444");
                Console.WriteLine("Entered Pin");
                Thread.Sleep(3000);
                pinSubmitButton.Click();

                Thread.Sleep(8000);

                var tqSection = driver.FindElementById("tqSection");

                Assert.IsTrue(tqSection.Displayed, "Thanks you for subscribing is not Displayed");
                Console.WriteLine("Users is Subscribed to sammedia");

                driver.Quit();


            }




        }
    }
}
