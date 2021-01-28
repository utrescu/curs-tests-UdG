using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace ElTempsTest
{
    public class UnitTest1 : IDisposable
    {
        ChromeDriver driver;

        public UnitTest1()
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
        public void TestQueSiAccedimAlSistemaSenseIdentificarEnsRetornaUnError()
        {
            // GIVEN un accés sense usuari

            // WHEN Anem a la pàgina principal
            driver.Navigate().GoToUrl("https://localhost:5001");

            Thread.Sleep(500);
            // THEN
            var error = driver.FindElementById("errortype").Text;
            var missatge = driver.FindElement(By.Id("errormessage")).Text;


            Assert.Equal("Error", error);
            Assert.StartsWith("Identifica't", missatge);
        }

        [Theory]
        [InlineData("inexistent@localhost", "Hola!123")]        
        public void TestQueSiFaigLoginAmbUnUsuariIncorrecteRebemUnError(string usuari, string contrasenya)
        {
            // GIVEN un usuari que vol fer login                     
            driver.Url = "https://localhost:5001/Identity/Account/Login";

            // WHEN intenta entrar
            driver.FindElement(By.Id("correu")).SendKeys(usuari);
            driver.FindElement(By.Id("contrasenya")).SendKeys(contrasenya);
            driver.FindElement(By.Id("entrar")).Click();

            // THEN            
            var errors = driver.FindElements(By.ClassName("validation-summary-errors"))
                .Select(err => err.Text);

            Assert.Contains("Invalid login attempt.", errors);
        }

        [Fact]
        public void TestQueSiFaigLoginAmbUnUsuariSenseContrasenyaNoEntra()
        {
            // GIVEN un usuari que vol fer login
            var usuari = "pere@udg.edu";
            var contrasenya = "";
            driver.Url = "https://localhost:5001/Identity/Account/Login";

            // WHEN intenta entrar
            driver.FindElement(By.Id("correu")).SendKeys(usuari);
            driver.FindElement(By.Id("contrasenya")).SendKeys(contrasenya);
            driver.FindElement(By.Id("entrar")).Click();

            // THEN            
            var errors = driver.FindElements(By.ClassName("validation-summary-errors"))
                .Select(err => err.Text);

            Assert.Contains("The Password field is required.", errors);
        }

        [Theory]
        [InlineData("pepet@", "Hola!123")]
        [InlineData("pepet", "Hola!123")]
        public void TestQueSiFaigLoginAmbCorreuIncorrecteRebemUnError(string usuari, string contrasenya)
        {
            // GIVEN un usuari que vol fer login                     
            driver.Url = "https://localhost:5001/Identity/Account/Login";

            // WHEN intenta entrar
            driver.FindElement(By.Id("correu")).SendKeys(usuari);
            driver.FindElement(By.Id("contrasenya")).SendKeys(contrasenya);
            driver.FindElement(By.Id("entrar")).Click();

            // THEN el correu està malament                        
            var errorMessage = driver.FindElement(By.Id("correu")).GetAttribute("validationMessage");
            Assert.NotEmpty(errorMessage);
        }

    }
}
