namespace POMexample.TestApplications.OrangeHRM.Locators;

// Nombre del diccionario.
public class LoginLocators
{
    public static readonly Dictionary<String, String> Locators = new()
    {
        { "usernameInput_Xpath", "//input[@name='username']" },
        { "passwordInput_Xpath", "//input[@name='password']" },
        { "loginButton_Xpath", "//button[@type='submit']" }
    };
}