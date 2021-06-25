using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace TestingExpenses
{
    class End2endTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetupDriver()
        {
            _driver = new ChromeDriver("D:\\cursPostuniv");
        }


        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
        [Test]
        public void loginAndViewExpenses()
        {
            _driver.Url = "http://localhost:4200/login";


            
            var emailInput = _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/form/ion-item[1]/ion-input/input"));
            emailInput.SendKeys("maria@yahoo.com");
            System.Threading.Thread.Sleep(2000);

            var password = _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/form/ion-item[2]/ion-input/input"));
            System.Threading.Thread.Sleep(2000);
            password.SendKeys("Ana123!");

            var loginBtn = _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/form/ion-button"));
            loginBtn.Click();
            System.Threading.Thread.Sleep(2000);

            _driver.FindElement(By.XPath("//ion-list"));
            System.Threading.Thread.Sleep(4000);
            Assert.Pass();

            

        }
    }
}
