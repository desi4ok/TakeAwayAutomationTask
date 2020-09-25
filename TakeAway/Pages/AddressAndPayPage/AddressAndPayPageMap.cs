namespace TakeAway.Pages.AddressAndPayPage
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System.Collections.ObjectModel;

    public partial class AddressAndPayPage
    {
        //Gets the elements of input fields for typing the personal details
        public IWebElement Address => wait.Until((d) => { return d.FindElement(By.CssSelector("#iaddress")); });

        public IWebElement PostCode => wait.Until((d) => { return d.FindElement(By.CssSelector("#ipostcode")); });

        public IWebElement City => wait.Until((d) => { return d.FindElement(By.CssSelector("#itown")); });

        public IWebElement Name => wait.Until((d) => { return d.FindElement(By.CssSelector("#isurname")); });

        public IWebElement Email => wait.Until((d) => { return d.FindElement(By.CssSelector("#iemail")); });

        public IWebElement PhoneNumber => wait.Until((d) => { return d.FindElement(By.CssSelector("#iphonenumber")); });

        //Gets the elements in dropdown for payment amount
        public IWebElement PaymentAmount => wait.Until((d) => { return d.FindElement(By.CssSelector("#ipayswith")); });
        public SelectElement PaymentAmountOption => new SelectElement(this.PaymentAmount);

        // Gets the button element for delivery time
        public IWebElement DeliveryTime => wait.Until((d) => { return d.FindElement(By.CssSelector("#ideliverytimen")); });
        public SelectElement DeliveryTimeOption => new SelectElement(this.DeliveryTime);

        //Gets the button element for Order and Pay
        public IWebElement OrderButton => wait.Until((d) => { return d.FindElement(By.CssSelector(".button_form.cartbutton-button")); });

        //Gets the error message element
        public IWebElement ErrorMessage => wait.Until((d) => { return d.FindElement(By.CssSelector(".notificationalert.notificationfeedbackwrapper")); });
    }
}

