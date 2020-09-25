namespace TakeAway.Pages.TestRestaurantPage
{
    using OpenQA.Selenium;

    public class TestRestaurantPage : BasePage
    {
        public TestRestaurantPage(IWebDriver driver) : base(driver) { }

        // Gets any item without side dish
        public IWebElement ItemWithoutSideDish => wait.Until((d) => { return d.FindElement(By.CssSelector(".meal__wrapper:not(.js-has-sidedishes)")); });

        // Gets any item with side dish
        public IWebElement ItemWithSideDish => wait.Until((d) => { return d.FindElement(By.CssSelector(".meal__wrapper.js-has-sidedishes")); });

        // Gets the button element for order item with side dish
        public IWebElement OrderSideDishButton => wait.Until((d) => { return d.FindElement(By.CssSelector(".cartbutton-button.cartbutton-button-sidedishes.add-btn-icon")); });
        
        //Gets the button for Order
        public IWebElement OrderButton => wait.Until((d) => { return d.FindElement(By.CssSelector(".basket__order-button.cartbutton-button")); });
    }
}
