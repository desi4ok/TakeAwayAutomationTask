namespace TakeAway.Pages.TestRestaurantPage
{
    using OpenQA.Selenium;

    public class TestRestaurantPage : BasePage
    {
        public TestRestaurantPage(IWebDriver driver) : base(driver) { }

        // Gets any item without side dish
        public IWebElement ItemWithoutSideDish => Wait.Until((d) => { return (this.GetElementByCssSelector(".meal__wrapper:not(.js-has-sidedishes)")); });

        // Gets any item with side dish
        public IWebElement ItemWithSideDish => Wait.Until((d) => { return (this.GetElementByCssSelector(".meal__wrapper.js-has-sidedishes")); });

        // Gets the button element for order item with side dish
        public IWebElement OrderSideDishButton => Wait.Until((d) => { return (this.GetElementByCssSelector(".cartbutton-button.cartbutton-button-sidedishes.add-btn-icon")); });

        //Gets the button for Order
        public IWebElement OrderButton => Wait.Until((d) => { return (this.GetElementByCssSelector(".basket__order-button.cartbutton-button")); });
    }
}
