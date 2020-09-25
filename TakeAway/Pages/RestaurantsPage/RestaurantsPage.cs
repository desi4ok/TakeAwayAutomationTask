namespace TakeAway.Pages.RestaurantsPage
{
    using OpenQA.Selenium;
    using System.Collections.ObjectModel;

    public class RestaurantsPage : BasePage
    {
        public RestaurantsPage(IWebDriver driver) : base(driver) { }

        //Gets the number of listed restaurants
        public IWebElement GetListedNumberOfRestaurants => wait.Until((d) => { return d.FindElement(By.CssSelector(".restaurant-amount")); });

        //Gets restaurant element different from template restaurant
        public IWebElement TestRestaurant => wait.Until((d) => { return d.FindElement(By.CssSelector(".js-restaurant.restaurant:not(#SingleRestaurantTemplateIdentifier)")); });

        public ReadOnlyCollection<IWebElement> CollectionOfRestaurants => wait.Until((d) => 
            {
               return d.FindElements(By.CssSelector(".js-restaurant.restaurant:not(#SingleRestaurantTemplateIdentifier)"));
            });
    }
}
