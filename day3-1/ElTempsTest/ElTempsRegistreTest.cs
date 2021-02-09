using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace ElTempsTestNamespace
{
    public class ElTempsTest : IDisposable
    {
        ChromeDriver driver;

        public ElTempsTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--lang=ca");
            driver = new ChromeDriver(".", options);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
        }

        public void Dispose()
        {
            driver.Quit();
        }

        [Fact]
        public void ComprovaQueNoEsPotRegistrarUnUsuariQueNoExisteix() 
        {
            // GIVEN un usuari que vol registrar-se                     
            driver.Url = "https://localhost:5001/Identity/Account/Register";

            // WHEN

        }
    }
}
