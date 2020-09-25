namespace TakeAway.Pages
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Reflection;

    public class BasePage
    {
        private IWebDriver driver;
        public WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

       public string Url { get; protected set; }

       // Navigates to the page
        public void NavigateTo()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}

