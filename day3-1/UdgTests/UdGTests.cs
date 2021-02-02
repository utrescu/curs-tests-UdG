using System;
using Xunit;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Linq;

namespace Testemps
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var driver = new ChromeDriver();
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);

            driver.Navigate().GoToUrl("https://www.udg.edu");

            driver.FindElementByClassName("main-header__search-toggle-icon").Click();

            var busca = driver.FindElementByClassName("main-header__search-input");
            busca.SendKeys("Informàtica");

            driver.FindElementByClassName("main-header__search-button").Click();

            Thread.Sleep(5000);

            var links = driver.FindElements(By.CssSelector("a.gs-title"));

            var url  = links[0].GetAttribute("href");
            links[0].Click();

            Assert.Equal(driver.Url, url);


            driver.Quit();
       }
    }
}
