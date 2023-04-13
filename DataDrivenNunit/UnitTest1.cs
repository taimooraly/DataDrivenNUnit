using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DataDrivenNunit
{
    [TestFixture]
    public class DataDriven
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://adactinhotelapp.com/");
        }

        [Test]
        [TestCaseSource(typeof(TestData), "LoginDetails")]
        public void Login(string username, string password)
        {
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);                
            driver.FindElement(By.Id("login")).Click();
        }
        [TearDown]
        public void Teardown()
        {
            driver.Close();
        }
    }
    public class TestData
    {
        public static object[] LoginDetails
        {
            get
            {
                string path = @"C:\Users\Syed Taimoor Ali\source\repos\DataDrivenNunit\DataDrivenNunit\bin\Debug\net6.0\TestData.json";
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    dynamic data = JsonConvert.DeserializeObject(json);
                    List<object[]> loginDataList = new List<object[]>();
                    foreach (var loginObject in data)
                    {
                        string username = loginObject.username;
                        string password = loginObject.password;
                        loginDataList.Add(new object[] { username, password });
                    }
                    return loginDataList.ToArray();
                }
            }
        }


    }
}