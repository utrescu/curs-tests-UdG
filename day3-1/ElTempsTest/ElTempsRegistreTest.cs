using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace eltemps
{
    public class ElTempsRegistreTest : IDisposable
    {
        ChromeDriver driver;
        WebDriverWait wait;

        private const string url = "https://localhost:5001/Identity/Account/Register";

        public ElTempsRegistreTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--lang=ca");
            driver = new ChromeDriver(".", options);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
        }

        public void Dispose()
        {
            driver.Quit();
        }

        [Theory]
        [InlineData("Nom", "", "Gallerí", "pere@udg.edu", "Udg2021!")]
        [InlineData("Cognoms", "Pere", "", "pere@udg.edu", "Udg2021!")]
        [InlineData("Correu", "Pere", "Gallerí", "", "Udg2021!")]
        [InlineData("Contrasenya", "Pere", "Gallerí", "pere@udg.edu", "")]
        public void ComprovaQueNoEsPotRegistrarNingúSenseNomNiCognoms(
            string camp,
            string nom,
            string cognoms,
            string correu,
            string contrasenya)
        {
            // GIVEN un usuari que vol registrar-se
            var poblacio = "Bonyeta";            

            var expectedError = $"El {camp} és obligatòri";


            driver.Url = url;

            driver.FindElement(By.Id("nom")).SendKeys(nom);
            driver.FindElement(By.Id("cognoms")).SendKeys(cognoms);
            driver.FindElement(By.Id("poblacio")).SendKeys(poblacio);
            driver.FindElement(By.Id("correu")).SendKeys(correu);
            driver.FindElement(By.Id("contrasenya")).SendKeys(contrasenya);
            driver.FindElement(By.Id("confirmacio")).SendKeys(contrasenya);

            // WHEN ho prova
            driver.FindElement(By.Id("submit")).Click();

            // THEN Ha de rebre un error
            var errors = driver.FindElements(By.ClassName("field-validation-error"))
               .Select(err => err.Text).ToList();

            Assert.Contains(expectedError, errors);
        }
    
        [Theory]
        [InlineData("Casablanca")]
        [InlineData("Kabul")]
        [InlineData("Figueres")]
        public void ComprovaQueUnUsuariQueNoEsDeLaComarcaNoEsPotRegistrar(string poblacio)
        {
            // GIVEN un usuari que vol registrar-se
            var nom = Guid.NewGuid().ToString();
            var cognoms = nom;
            var correu = $"{nom}@udg.edu";
            var contrasenya = "Udg2021!";

            var expectedError = $"El poble '{poblacio}' no és de la comarca del Bonyetès";

            
            driver.Url = url;

            driver.FindElement(By.Id("nom")).SendKeys(nom);
            driver.FindElement(By.Id("cognoms")).SendKeys(cognoms);
            driver.FindElement(By.Id("poblacio")).SendKeys(poblacio);
            driver.FindElement(By.Id("correu")).SendKeys(correu);
            driver.FindElement(By.Id("contrasenya")).SendKeys(contrasenya);
            driver.FindElement(By.Id("confirmacio")).SendKeys(contrasenya);

            // WHEN ho prova
            driver.FindElement(By.Id("submit")).Click();

            // THEN Ha de rebre un error
            var errors = driver.FindElements(By.ClassName("field-validation-error"))
               .Select(err => err.Text);

            Assert.Contains(expectedError, errors);
        }


        [Theory]
        [InlineData("Udg2021!", "Udg2020!")]
        [InlineData("Udg2021!", "")]
        public void ComprovaQueUnUsuariNoEsRegistraSiLaContrasenyaNoQuadra(string contrasenya, string confirmacio)
        {
            // GIVEN un usuari que vol registrar-se
            var nom = Guid.NewGuid().ToString();
            var cognoms = nom;
            var correu = $"{nom}@udg.edu";
            var poblacio = "Bonyeta";

            var expectedError = $"Les contrasenyes no quadren.";


            driver.Url = url;

            driver.FindElement(By.Id("nom")).SendKeys(nom);
            driver.FindElement(By.Id("cognoms")).SendKeys(cognoms);
            driver.FindElement(By.Id("poblacio")).SendKeys(poblacio);
            driver.FindElement(By.Id("correu")).SendKeys(correu);
            driver.FindElement(By.Id("contrasenya")).SendKeys(contrasenya);
            driver.FindElement(By.Id("confirmacio")).SendKeys(confirmacio);

            // WHEN ho prova
            driver.FindElement(By.Id("submit")).Click();

            // THEN Ha de rebre un error
            var errors = driver.FindElements(By.ClassName("field-validation-error"))
               .Select(err => err.Text);

            Assert.Contains(expectedError, errors);
        }


        [Theory]
        [InlineData("a")]
        public void ComprovaQueNoEsPodenDefinirContrasenyesMassaCurtesNiBuides(string contrasenya)
        {
            // GIVEN un usuari que vol registrar-se
            var nom = Guid.NewGuid().ToString();
            var cognoms = nom;
            var correu = $"{nom}@udg.edu";
            var poblacio = "Bonyeta";

            var expectedError = $"La Contrasenya ha de tenir com a mínim 6 caràcters i com a màxim 100.";


            driver.Url = url;

            driver.FindElement(By.Id("nom")).SendKeys(nom);
            driver.FindElement(By.Id("cognoms")).SendKeys(cognoms);
            driver.FindElement(By.Id("poblacio")).SendKeys(poblacio);
            driver.FindElement(By.Id("correu")).SendKeys(correu);
            driver.FindElement(By.Id("contrasenya")).SendKeys(contrasenya);
            driver.FindElement(By.Id("confirmacio")).SendKeys(contrasenya);

            // WHEN ho prova
            driver.FindElement(By.Id("submit")).Click();

            // THEN Ha de rebre un error
            var errors = driver.FindElements(By.ClassName("field-validation-error"))
               .Select(err => err.Text);

            Assert.Contains(expectedError, errors);

        }

        [Theory]
        [InlineData("cognom cognom", "Bonyeta", "C1ntrasenya!")]
        public void ComprovaQueEsPotRegistrarUnUsuariQueNoExisteix(string cognoms, string poblacio, string contrasenya)
        {
            var nom = Guid.NewGuid().ToString();
            var correu = $"{nom}@udg.edu";

            // GIVEN un usuari que vol registrar-se
            driver.Url = url;

            driver.FindElement(By.Id("nom")).SendKeys(nom);
            driver.FindElement(By.Id("cognoms")).SendKeys(cognoms);
            driver.FindElement(By.Id("poblacio")).SendKeys(poblacio);
            driver.FindElement(By.Id("correu")).SendKeys(correu);
            driver.FindElement(By.Id("contrasenya")).SendKeys(contrasenya);
            driver.FindElement(By.Id("confirmacio")).SendKeys(contrasenya);

            // WHEN
            driver.FindElement(By.Id("submit")).Click();


            // THEN
            wait.Until((driver) => driver.Url != url);
        }
    }
}
