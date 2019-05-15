using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public static class WebDriverUtil
    {


        public static void Click(this IWebElement element, IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));

            element.Click();
        }


        public static void SetText(this IWebElement element, IWebDriver driver, string textToEnter)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));

            element.Clear();
            element.SendKeys(textToEnter);

        }


        public static string GetText(this IWebElement element, IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
          
            if(element.TagName == "input")
            {
                return element.GetAttribute("value");
            }
            else
            {
                return element.Text;
            }
           
        }

        public static void PickDropdownByText(this IWebElement element, IWebDriver driver, string choice)
        {

            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByValue(choice);

        }
    }
}
