namespace POMexample.TestApplications.SwagLabs.Locators;

// Nombre del diccionario.
public class CartLocators
{
    public static readonly Dictionary<String, String> Locators = new()
    {
        { "cartLabel_Xpath", "//span[@class='title' and text()='Your Cart']" },
        { "cartLink_Id", "shopping_cart_container" },
        { "checkoutButton_Id", "checkout" }
    };
}