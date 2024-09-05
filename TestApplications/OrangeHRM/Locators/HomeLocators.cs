namespace POMexample.TestApplications.OrangeHRM.Locators;

// Nombre del diccionario.
public class HomeLocators
{
    public static readonly Dictionary<String, String> Locators = new()
    {
        { "homeLabel_Xpath", "//span//h6[text()='Dashboard']" },
        { "userDropdown_Xpath", "//i[@class='oxd-icon bi-caret-down-fill oxd-userdropdown-icon']" },
        { "logoutLink_Xpath", "//a[text()='Logout']" }
    };
}