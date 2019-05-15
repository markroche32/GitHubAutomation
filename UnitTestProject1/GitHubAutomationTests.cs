using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    [TestClass]
    public class GitHubAutomationTests
    {
        private TestContext testContextInstance;
        private IWebDriver driver;
        
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [TestInitialize()]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();      
            driver.Navigate().GoToUrl("https://github.com/");
          
        }

        [TestMethod]
        public void SignInShouldRedirectToLogin()
        {

            new GitHubPageModel(driver).SignIn.Click(driver);
            Assert.IsTrue(driver.Url.Contains("/login"));
        }

        [TestMethod]
        public void UsernameAndPasswordShouldBeMandatory()
        {         
            new GitHubPageModel(driver).SignIn.Click(driver);

            //Initialy, Before LoginSignIn is clicked no Username / Password error message displayed
            //LoginError MSG will not be displayed => Expected, NoSuchElementException
            try
            {
                new GitHubPageModel(driver).LoginError.GetAttribute("innerText");
                Assert.Fail("LoginError MSG Should Have Not been displayed");
            }
            catch (NoSuchElementException ex)
            {
                new GitHubPageModel(driver).LoginSignIn.Click(driver);

                //Test will fail with NoSuchElementException if LoginError element is not found
                Assert.IsTrue(new GitHubPageModel(driver).LoginError.GetText(driver).Contains("Incorrect username or password"));
            }
        }


        [TestMethod]
        public void ResetPasswordWithUnknownEmailShouldDisplayCantFindEmail()
        {
            new GitHubPageModel(driver).SignIn.Click(driver);
            new GitHubPageModel(driver).ForgetPassword.Click(driver);
            new GitHubPageModel(driver).EmailReset.SetText(driver,"m.ie");
            new GitHubPageModel(driver).SendPassResetEmail.Click(driver);

            string errorCantFindEmail = new GitHubPageModel(driver).CantFindEmailErrorMsg.GetText(driver);
            Assert.IsTrue(errorCantFindEmail.Contains("Can't find that email, sorry"));
        }


        [TestMethod]
        public void ResetPasswordWithEmptyEmailShouldDisplayCantFindEmail()
        {
            new GitHubPageModel(driver).SignIn.Click(driver);
            new GitHubPageModel(driver).ForgetPassword.Click(driver);
            new GitHubPageModel(driver).EmailReset.SetText(driver, " ");
            new GitHubPageModel(driver).SendPassResetEmail.Click(driver);

            string errorCantFindEmail = new GitHubPageModel(driver).CantFindEmailErrorMsg.GetText(driver);
            Assert.IsTrue(errorCantFindEmail.Contains("Can't find that email, sorry"));
        }


        [TestMethod]
        public void ResetPasswordWithUnknownEmailStartsWithCant()
        {
            new GitHubPageModel(driver).SignIn.Click(driver);
            new GitHubPageModel(driver).ForgetPassword.Click(driver);
            new GitHubPageModel(driver).EmailReset.SetText(driver, " ");
            new GitHubPageModel(driver).SendPassResetEmail.Click(driver);

            string errorCantFindEmail = new GitHubPageModel(driver).CantFindEmailErrorMsg.GetText(driver);
            Assert.IsTrue(errorCantFindEmail.Trim().StartsWith("Can't"));
        }


        [TestMethod]
        public void SignUpShouldRedirectToJoinPage()
        {

            new GitHubPageModel(driver).SignUp.Click(driver);
            Assert.IsTrue(driver.Url.Contains("/join"));
        }

        [TestMethod]
        public void JoinGithubPageShouldContainCreatePersonalAccountText()
        {

            new GitHubPageModel(driver).SignUp.Click(driver);
            Assert.IsTrue(driver.Url.Contains("/join"));
            string p = new GitHubPageModel(driver).CreatePersonalAccountHeader.GetText(driver);
            new GitHubPageModel(driver).CreatePersonalAccountHeader.GetText(driver);
        }


        [TestMethod]
        public void JoinGithubSignUpButtonShouldBeDisabledForExistingUserEmail()
        {

            new GitHubPageModel(driver).SignUp.Click(driver);

            //Verify Join with valid user details and non existent email => joinSignUp button is enabled/clickable
            new GitHubPageModel(driver).JoinUsername.SetText(driver, "MarkRoche314159");
            new GitHubPageModel(driver).JoinPassword.SetText(driver, "MarkRoche314159");
            new GitHubPageModel(driver).JoinEmail.SetText(driver, "MarkRoche314159@gmail.com");

            //Wait For joinSignUp button to be enabled/clickable => selenium is executing too fast
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(new GitHubPageModel(driver).JoinSignUp));

            //Assert.IsTrue(string.IsNullOrEmpty(new GitHubPageModel(driver).JoinSignUp.GetAttribute("disabled")));

            //joinSignUp button is enabled/clickable => as user details/email passes validation
            Assert.IsTrue(new GitHubPageModel(driver).JoinSignUp.Enabled);

            //Verify Join with valid user details and EXISTING Email => joinSignUp button is disabled
            new GitHubPageModel(driver).JoinUsername.SetText(driver, "MarkRoche314159");
            new GitHubPageModel(driver).JoinPassword.SetText(driver, "MarkRoche314159");
            new GitHubPageModel(driver).JoinEmail.SetText(driver, "markroche32@gmail.com");

            //Wait For joinSignUp button element to change in DOM and disabled attribute gets added => selenium is executing too fast
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//*[@id='signup_button'and @disabled]")));

            //joinSignUp button is disabled => disabled attribute is present in DOM => true
            Assert.IsFalse(new GitHubPageModel(driver).JoinSignUp.Enabled);
          


        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }

    }
}
