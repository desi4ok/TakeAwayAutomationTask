namespace TakeAway.Pages.AddressAndPayPage
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System.Collections.ObjectModel;

    public partial class AddressAndPayPage
    {
        //Gets the elements of input fields for typing the personal details
        public IWebElement Address => Wait.Until((d) => { return (this.GetElementByCssSelector("#iaddress")); });

        public IWebElement PostCode => Wait.Until((d) => { return (this.GetElementByCssSelector("#ipostcode")); });

        public IWebElement City => Wait.Until((d) => { return (this.GetElementByCssSelector("#itown")); });

        public IWebElement Name => Wait.Until((d) => { return (this.GetElementByCssSelector("#isurname")); });

        public IWebElement Email => Wait.Until((d) => { return (this.GetElementByCssSelector("#iemail")); });

        public IWebElement PhoneNumber => Wait.Until((d) => { return (this.GetElementByCssSelector("#iphonenumber")); });

        //Gets the elements in dropdown for payment amount
        public IWebElement PaymentAmount => Wait.Until((d) => { return (this.GetElementByCssSelector("#ipayswith")); });
        public SelectElement PaymentAmountOption => new SelectElement(this.PaymentAmount);

        //Gets the button element for Order and Pay
        public IWebElement OrderButton => Wait.Until((d) => { return (this.GetElementByCssSelector(".button_form.cartbutton-button")); });

        //Gets the error message element
        public IWebElement ErrorMessage => Wait.Until((d) => { return (this.GetElementByCssSelector(".notificationalert.notificationfeedbackwrapper")); });
    }
}

