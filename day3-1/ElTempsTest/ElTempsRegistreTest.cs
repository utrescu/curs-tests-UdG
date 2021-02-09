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

        [Theory]
        [InlineData("nom", "cognom cognom", "Bonyeta", "nom@cognom.es", "Contrasenya!")]
        public void ComprovaQueNoEsPotRegistrarUnUsuariQueNoExisteix(string nom, string cognoms, string poblacio, string correu, string contrasenya)
        {
            // GIVEN un usuari que vol registrar-se
            driver.Url = "https://localhost:5001/Identity/Account/Register";

            driver.FindElement(By.Id("nom")).SendKeys(nom);
            driver.FindElement(By.Id("cognoms")).SendKeys(cognoms);
            driver.FindElement(By.Id("poblacio")).SendKeys(poblacio);
            driver.FindElement(By.Id("email")).SendKeys(correu);
            driver.FindElement(By.Id("contrasenya")).SendKeys(contrasenya);
            driver.FindElement(By.Id("confirmacio")).SendKeys(contrasenya);

            // WHEN
            driver.FindElement(By.Id("submit")).Click();


            // THEN


        }
    }
}
