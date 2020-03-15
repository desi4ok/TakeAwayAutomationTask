namespace TakeAway.Pages.OrderNumberPage
{
    using OpenQA.Selenium;
    using System.Collections.ObjectModel;

    public class OrderNumberPage : BasePage
    {
        public OrderNumberPage(IWebDriver driver) : base(driver) { }

        //Gets the element which contains the Order number
        public IWebElement GetOrderNumber => Wait.Until((d) => { return (this.GetElementByCssSelector(".order-purchaseid")); });
    }
}
