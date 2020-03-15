﻿namespace TakeAway
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using TakeAway.Models;
    using TakeAway.Pages.AddressAndPayPage;
    using TakeAway.Pages.HomePage;
    using TakeAway.Pages.OrderNumberPage;
    using TakeAway.Pages.RestaurantsPage;
    using TakeAway.Pages.TestRestaurantPage;

    public class TakeAwayIntegrationTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private HomePage homePage;
        private RestaurantsPage restaurantsPage;
        private TestRestaurantPage testRestaurantPage;
        private AddressAndPayPage addressAndPayPage;
        private OrderNumberPage orderNumberPage;

        static string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;

        string validUserDetailsPath = projectDirectory + "/../../../TakeAway/JSONs/ValidPersonalDetails.json";
        string invalidPhoneNumberPath = projectDirectory + "/../../../TakeAway/JSONs/InvalidPhoneNumber.json";
        string invalidEmailPath = projectDirectory + "/../../../TakeAway/JSONs/InvalidEmail.json";

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            //driver = new FirefoxDriver(@"C:\TMP");
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            homePage = new HomePage(driver);
            restaurantsPage = new RestaurantsPage(driver);
            testRestaurantPage = new TestRestaurantPage(driver);
            addressAndPayPage = new AddressAndPayPage(driver);
            orderNumberPage = new OrderNumberPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Quit();
        }

        [Test]
        public void UserCanOrderIfEverythingIsFilledCorrectly()
        {
            var path = Path.GetFullPath(validUserDetailsPath);
            var details = PersonalDetails.FromJson(File.ReadAllText(path));

            // Navigate to the page
            homePage.NavigateTo();

            // Click on input field
            homePage.InputPostCode.Click();

            // Input the Post Code "8888", click on the suggested location
            homePage.InputPostCode.SendKeys("8888");
            homePage.PostCodeSuggestion.Click();

            // Check the address exists and click on it
            IWebElement addressToSelect = homePage.SelectAddress("8888 Alpha");
            Assert.IsNotNull(addressToSelect, "Address '8888 Alpha' does not exist");
            addressToSelect.Click();

            // Get the number of restaurants cards in the html
            int restaurantsCount = restaurantsPage.GetCollectionOfElementsByCssSelector(".js-restaurant.restaurant:not(#SingleRestaurantTemplateIdentifier)").Count;

            // Get the number of restaurants from the label "Order from X restaurants"
            int listedRestaurantsCount = int.Parse(restaurantsPage.GetListedNumberOfRestaurants.Text);

            // Assert that there is at least 1 restaurant listed
            Assert.IsTrue(restaurantsCount > 0, "There are no restaurants listed");

            // Assert that the number of cards and the number in the title are the same
            Assert.IsTrue(
                restaurantsCount == listedRestaurantsCount,
                "The number of listed restaurants and the restaurants cards do not match"
            );

            // Select the first restaurant from the list
            restaurantsPage.TestRestaurant.Click();

            // Get a menu item without a side dish
            IWebElement itemWithoutSideDish = testRestaurantPage.ItemWithoutSideDish;

            // Get a menu item with a side dish
            IWebElement itemWithSideDish = testRestaurantPage.ItemWithSideDish;

            //Check if there are dishes in the menu
            if (itemWithoutSideDish == null && itemWithSideDish == null)
            {
                Assert.Fail("There are no dishes in the selected restaurant");
            }

            // Add an item from the menu that hasn't got a side dish (if any)
            if (itemWithoutSideDish != null)
            {
                itemWithoutSideDish.Click();
            }

            // Add an item from the menu with a side dish with the default values (if any)
            if (itemWithSideDish != null)
            {
                itemWithSideDish.Click();
                testRestaurantPage.OrderSideDishButton.Click();
            }

            // Order the items added in the cart
            testRestaurantPage.OrderButton.Click();

            // Fill the contact and delivery details in the form
            addressAndPayPage.FillPersonalDetails(details);

            // Select the closest higher amount to pay
            addressAndPayPage.PaymentAmountOption.SelectByIndex(1);

            // Click on the "Order" button
            addressAndPayPage.OrderButton.Click();

            // Get the Order Id
            string orderNumber = orderNumberPage.GetOrderNumber.Text;
            Console.WriteLine(orderNumber);
        }

        [Test]
        public void NavigateToRestaurantsPageUsingShowButton()
        {
            homePage.NavigateTo();
            homePage.InputPostCode.Click();
            homePage.InputPostCode.SendKeys("8888");

            IWebElement submitPostCodeButton = homePage.SubmitPostCodeButton;

            //Check if the button exists
            if (submitPostCodeButton.GetAttribute("style").Contains("display: none"))
            {
                Assert.Fail("The 'Show' button is not visible");
            }

            //Click on the Show button
            submitPostCodeButton.Click();

            homePage.PostCodeSuggestion.Click();
            homePage.SelectAddress("8888 Alpha").Click();
        }

        [Test]
        public void VerifyThatUserCannotOrderWithEmptyBasket()
        {
            homePage.NavigateTo();
            homePage.InputPostCode.Click();
            homePage.InputPostCode.SendKeys("8888");
            homePage.PostCodeSuggestion.Click();
            homePage.SelectAddress("8888 Alpha").Click();
            restaurantsPage.TestRestaurant.Click();
            Assert.IsTrue(testRestaurantPage.OrderButton.GetAttribute("class").Contains("btn-disabled"));
        }

        [Test]
        public void VerifyThatUserCannotOrderWithEmptyPersonalDetails()
        {
            homePage.NavigateTo();
            homePage.InputPostCode.Click();
            homePage.InputPostCode.SendKeys("8888");
            homePage.PostCodeSuggestion.Click();
            homePage.SelectAddress("8888 Alpha").Click();
            restaurantsPage.TestRestaurant.Click();
            testRestaurantPage.ItemWithoutSideDish.Click();
            testRestaurantPage.ItemWithSideDish.Click();
            testRestaurantPage.OrderSideDishButton.Click();
            testRestaurantPage.OrderButton.Click();
            addressAndPayPage.Address.Clear();
            addressAndPayPage.PostCode.Clear();
            addressAndPayPage.City.Clear();
            addressAndPayPage.Name.Clear();
            addressAndPayPage.Email.Clear();
            addressAndPayPage.PhoneNumber.Clear();
            addressAndPayPage.OrderButton.Click();

            string errorMessage = addressAndPayPage.ErrorMessage.Text.Trim();
            string trueErrorMessage = "Please fill out your Name, Telephone Number, Email Address, delivery address, Postcode and town .";
            Assert.IsTrue(errorMessage == trueErrorMessage);
        }

        [Test]
        public void VerifyThatUserCannotOrderWithInvalidPhoneNumber()
        {
            var path = Path.GetFullPath(invalidPhoneNumberPath);
            var details = PersonalDetails.FromJson(File.ReadAllText(path));

            homePage.NavigateTo();
            homePage.InputPostCode.Click();
            homePage.InputPostCode.SendKeys("8888");
            homePage.PostCodeSuggestion.Click();
            homePage.SelectAddress("8888 Alpha").Click();
            restaurantsPage.TestRestaurant.Click();
            testRestaurantPage.ItemWithoutSideDish.Click();
            testRestaurantPage.ItemWithSideDish.Click();
            testRestaurantPage.OrderSideDishButton.Click();
            testRestaurantPage.OrderButton.Click();
            addressAndPayPage.FillPersonalDetails(details);
            addressAndPayPage.OrderButton.Click();

            // Check if the order was processed and the user was navigated to the confirmation page            string finishedOrderBaseUrl = "https://www.thuisbezorgd.nl/en/ordered-at-androidlicious#";
            string currentUrl = driver.Url;

            if (currentUrl == finishedOrderBaseUrl)
            {
                Assert.Fail("Order was completed with invalid phone number.");
            }
        }

        [Test]
        public void VerifyThatUserCannotOrderWithInvalidEmail()
        {
            var path = Path.GetFullPath(invalidEmailPath);
            var details = PersonalDetails.FromJson(File.ReadAllText(path));

            homePage.NavigateTo();
            homePage.InputPostCode.Click();
            homePage.InputPostCode.SendKeys("8888");
            homePage.PostCodeSuggestion.Click();
            homePage.SelectAddress("8888 Alpha").Click();
            restaurantsPage.TestRestaurant.Click();
            testRestaurantPage.ItemWithoutSideDish.Click();
            testRestaurantPage.ItemWithSideDish.Click();
            testRestaurantPage.OrderSideDishButton.Click();
            testRestaurantPage.OrderButton.Click();
            addressAndPayPage.FillPersonalDetails(details);
            addressAndPayPage.OrderButton.Click();

            // Check if the order was processed and the user was navigated to the confirmation page
            string finishedOrderBaseUrl = "https://www.thuisbezorgd.nl/en/ordered-at-androidlicious#";
            string currentUrl = driver.Url;

            if (currentUrl == finishedOrderBaseUrl)
            {
                Assert.Fail("Order was completed with invalid email.");
            }
        }
    }
}

