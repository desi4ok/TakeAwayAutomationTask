namespace TakeAway.Pages.HomePage
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System.Collections.ObjectModel;

    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
            Url = " https://www.thuisbezorgd.nl/en/";
        }

        // Gets the element for typing the post code
        public IWebElement InputPostCode => wait.Until((d) => { return d.FindElement(By.CssSelector("#imysearchstring")); });

        // Gets the Post Code suggestion element
        public IWebElement PostCodeSuggestion => wait.Until((d) => { return d.FindElement(By.CssSelector(".lp__place.selected")); });

        //Gets the button element for post code submit
        public IWebElement SubmitPostCodeButton => wait.Until((d) => { return d.FindElement(By.CssSelector("#submit_deliveryarea")); });

        // Gets all listed addresses from the Search combobox and returns the matched element
        public IWebElement SelectAddress(string searchAddresss)
        {
            IWebElement addressElement = wait.Until((d) =>
            {
                IWebElement element = null;
                ReadOnlyCollection<IWebElement> addresses = d.FindElements(By.CssSelector("#reference > span"));

                foreach (IWebElement address in addresses)
                {
                    string text = address.Text.Trim();

                    if (text == searchAddresss)
                    {
                        element = address;
                    }
                }

                return element;
            });

            return addressElement;
        }
    }
}
