

using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;

namespace UnitTestProject1
{
    class GitHubPageModel
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = @"user_password")]
        public IWebElement JoinPassword { get; set; }

        [FindsBy(How = How.Id, Using = @"user_login")]
        public IWebElement JoinUsername { get; set; }

        [FindsBy(How = How.Id, Using = @"user_email")]
        public IWebElement JoinEmail { get; set; }

        [FindsBy(How = How.Id, Using = @"signup_button")]
        public IWebElement JoinSignUp { get; set; }

        [FindsBy(How = How.Id, Using = @"login_field")]
        public IWebElement Username { get; set; }

        [FindsBy(How = How.Id, Using = @"password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Name, Using = @"commit")]
        public IWebElement LoginSignIn { get; set; }

        [FindsBy(How = How.XPath, Using = @"//a[contains(text(),'Sign') and contains(text(),'in')]")]
        public IWebElement SignIn { get; set; }


        [FindsBy(How = How.XPath, Using = @"//a[contains(text(),'Sign') and contains(text(),'up')][contains(@class, 'HeaderMenu-link')]")]
        public IWebElement SignUp { get; set; }

        [FindsBy(How = How.XPath, Using = @"//h2[contains(text(), 'Create your personal account')]")]
        public IWebElement CreatePersonalAccountHeader { get; set; }

        [FindsBy(How = How.XPath, Using = @"//*[text()[contains(., 'Incorrect username or password.')]]")]
        public IWebElement LoginError { get; set; }

        [FindsBy(How = How.Id, Using = @"email_field")]
        public IWebElement EmailReset { get; set; }

        [FindsBy(How = How.XPath, Using = @"//a[contains(text(),'Forgot password')]")]
        public IWebElement ForgetPassword { get; set; }

        [FindsBy(How = How.XPath, Using = @"//input[@value='Send password reset email']")]
        public IWebElement SendPassResetEmail { get; set; }

        [FindsBy(How = How.XPath, Using = @"//div[@id='js-flash-container']//div[@class='container']")]
        public IWebElement CantFindEmailErrorMsg { get; set; }


        public GitHubPageModel(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void LoginToApplication()
        {
      
        }


        public IWebElement DestinationListItem(string destination)
        {

            return driver.FindElement(By.XPath("//li//b[contains(text(),'" + destination + "')]")); ;

        }


        public IWebElement SelectCalendarDayMonth(string day , string month)
        {

            return driver.FindElement(By.XPath("(//*[contains(text(), '" + month + "')]/ancestor::thead/following-sibling::tbody//td[contains(@class, 'c2-day')]//span[contains(text(),'" + day + "')])[1]")); 

        }


        public IWebElement ReturnSearchResultByHotel(string hotel)
        {

            return driver.FindElement(By.XPath("//span[contains(@class, 'sr-hotel__name') and contains(text(), '" + hotel + "')]"));

        }

        public bool ClickCalendarDayMonth(string day, string month)
        {
            var i = 0;

            //3 months from 2days date should be never be further than GoRight x 5
            while (i < 5)
            {
                try
                {
                    SelectCalendarDayMonth(day, month).Click(driver);
                    return true;
                }
                catch (Exception)
                {
                    //Date Is Not Visible => Clickable => Go Right 
                    
                    Thread.Sleep(2000);
                }
            }

            return false;
        }

    }

}
