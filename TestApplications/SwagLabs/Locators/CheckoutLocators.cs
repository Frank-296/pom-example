namespace POMexample.TestApplications.SwagLabs.Locators;

// Nombre del diccionario.
public class CheckoutLocators
{
    public static readonly Dictionary<String, String> Locators = new()
    {
        { "checkoutLabel_Xpath", "//span[@class='title' and text()='Checkout: Your Information']" },
        { "firstNameInput_Id", "first-name" },
        { "lastNameInput_Id", "last-name" },
        { "postalCodeInput_Id", "postal-code" },
        { "continueButton_Id", "continue" },
        { "overviewLabel_Xpath", "//span[@class='title' and text()='Checkout: Overview']" },
        { "productName_Xpath", "//div[@class='inventory_item_name'][text()='{0}']" },
        { "finishButton_Id", "finish" },
        { "purchaseLabel_Xpath", "//span[@class='title' and text()='Checkout: Complete!']" }
    };
}