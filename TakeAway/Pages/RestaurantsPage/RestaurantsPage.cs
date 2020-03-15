namespace TakeAway.Pages.RestaurantsPage
{
    using OpenQA.Selenium;
    using System.Collections.ObjectModel;

    public class RestaurantsPage : BasePage
    {
        public RestaurantsPage(IWebDriver driver) : base(driver) { }

        //Gets the number of listed restaurants
        public IWebElement GetListedNumberOfRestaurants => Wait.Until((d) => { return (this.GetElementByCssSelector(".restaurant-amount")); });

        //Gets restaurant element different from template restaurant
        public IWebElement TestRestaurant => Wait.Until((d) => { return (this.GetElementByCssSelector(".js-restaurant.restaurant:not(#SingleRestaurantTemplateIdentifier)")); });

        public ReadOnlyCollection<IWebElement> CollectionOfRestaurants => Wait.Until((d) => 
            {
                return (this.GetCollectionOfElementsByCssSelector(".js-restaurant.restaurant:not(#SingleRestaurantTemplateIdentifier)")); 
            });
    }
}
