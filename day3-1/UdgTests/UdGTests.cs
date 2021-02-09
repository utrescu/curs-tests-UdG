using System;
using Xunit;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace Testemps
{
    public class UnitTest1
    {
    //     [Fact]
    //     public void Test1()
    //     {
    //         var driver = new ChromeDriver();
    //         DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
    //         fluentWait.Timeout = TimeSpan.FromSeconds(5);
    //         fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);

    //         driver.Navigate().GoToUrl("https://www.udg.edu");

    //         driver.FindElementByClassName("main-header__search-toggle-icon").Click();

    //         var busca = driver.FindElementByClassName("main-header__search-input");
    //         busca.SendKeys("Informàtica");

    //         driver.FindElementByClassName("main-header__search-button").Click();

    //         Thread.Sleep(1000);

    //         var links = driver.FindElements(By.CssSelector("a.gs-title"));

    //         var url  = links[0].GetAttribute("href");
    //         links[0].Click();

    //         driver.Quit();
    //    }


       [Fact]
       public void Test2() 
       {
           Random rnd = new Random();

           var driver = new ChromeDriver();
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);

            driver.Navigate().GoToUrl("https://duckduckgo.com");
            var input = driver.FindElement(By.XPath("//input[@id='search_form_input_homepage']"));
            input.SendKeys("universitat de girona");
            input.Submit();

            

            for(int i=0; i <= 5; i++) {
                var links = driver.FindElementsByClassName("result__a");
                Thread.Sleep(1000);

                Actions actions = new Actions(driver);
                actions.MoveToElement(links[i]);
                actions.Perform();

                links[i].Click();
                Thread.Sleep(1000);
                driver.Navigate().Back();

            }

            Thread.Sleep(1000);

            driver.Quit();

       }
    }
}
