namespace TakeAway.Pages.OrderNumberPage
{
    using OpenQA.Selenium;
    using System.Collections.ObjectModel;

    public class OrderNumberPage : BasePage
    {
        public OrderNumberPage(IWebDriver driver) : base(driver) { }

        //Gets the element which contains the Order number
        public IWebElement GetOrderNumber => wait.Until((d) => { return d.FindElement(By.CssSelector(".order-purchaseid")); });
    }
}
