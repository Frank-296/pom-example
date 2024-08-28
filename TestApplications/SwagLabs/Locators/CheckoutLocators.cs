namespace POMexample.TestApplications.SwagLabs.Locators;

// Nombre del diccionario.
public class CheckoutLocators
{
    public static readonly Dictionary<String, String> Locators = new()
    {
        { "checkoutLabel_Xpath", "//span[@class='title' and text()='Checkout: Your Information']" }
    };
}