namespace TakeAway.Pages.AddressAndPayPage
{
    using OpenQA.Selenium;
    using System.Collections.ObjectModel;
    using TakeAway.Models;

    public partial class AddressAndPayPage : BasePage
    {
        public AddressAndPayPage(IWebDriver driver) : base(driver) { }

        //Method for filling the personal details form
        public void FillPersonalDetails(PersonalDetails details)
        {
            Address.Clear();
            Address.SendKeys(details.Address);
            PostCode.Clear();
            PostCode.SendKeys(details.PostCode);
            City.Clear();
            City.SendKeys(details.City);
            Name.Clear();
            Name.SendKeys(details.Name);
            PhoneNumber.Clear();
            PhoneNumber.SendKeys(details.PhoneNumber);
            Email.Clear();
            Email.SendKeys(details.Email);
        }
    }
}

