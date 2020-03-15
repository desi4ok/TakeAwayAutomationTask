﻿namespace TakeAway.Pages
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
        private IWebDriver _driver;
        

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebDriver Driver => _driver;

        public WebDriverWait Wait => new WebDriverWait(_driver, TimeSpan.FromSeconds(120));

        public string Url { get; protected set; }

        public string BaseUrl => "https://www.thuisbezorgd.nl";

        // Navigates to the page
        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(Url);
        }
        
        // Gets a collection of html elements matched by a CSS selector
        public ReadOnlyCollection<IWebElement> GetCollectionOfElementsByCssSelector(string selector)
        {
            ReadOnlyCollection<IWebElement> elements = Driver.FindElements(By.CssSelector(selector));

            return elements;
        }

        // Gets a single html element matched by a CSS selector
        public IWebElement GetElementByCssSelector(string selector)
        {
            IWebElement element = Driver.FindElement(By.CssSelector(selector));

            return element;
        }

        
    }
}

