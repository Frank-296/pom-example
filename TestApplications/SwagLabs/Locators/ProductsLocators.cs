namespace POMexample.TestApplications.SwagLabs.Locators;

// Nombre del diccionario.
public class ProductsLocators
{
    public static readonly Dictionary<String, String> Locators = new()
    {
        { "homeLabel_Xpath", "//div[text()='Swag Labs']" },
        // Este locator es interesante porque hay varios en la página que son similares
        // así que para diferenciarlo del resto utilizaremos el {0} para pasarle con "LINQ" el nombre del producto extraído del archivo Excel y de esa manera
        // hacer que el locator sea único.
        { "productOption_Xpath", "//div[@class='inventory_item_name ' and text()='{0}']" },
        // De la misma manera para este locator será necesario pasarle el nombre del producto en el {0}.
        { "productName_Xpath", "//div[@class='inventory_details_name large_size'][text()='{0}']" },
        { "addToCartButton_Id", "add-to-cart" },
    };
}