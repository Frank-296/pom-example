namespace POMexample.TestApplications.SwagLabs.Locators;

// Nombre del diccionario.
public class LoginLocators
{
    public static readonly Dictionary<String, String> Locators = new()
    {   // A mí me gusta nombrar los locators primero con el identificador, luego con el tipo de elemento web y finalmente con el tipo de búsqueda de Selenium,
        // por ejemplo:
        // username = identificador; Input = tipo de elemento web; _Id = tipo de búsqueda de Selenium.
        // password = identificador; Input = tipo de elemento web; _Id = tipo de búsqueda de Selenium.
        // login = identificador; Button = tipo de elemento web; _Id = tipo de búsqueda de Selenium.
        { "usernameInput_Id", "user-name" },
        { "passwordInput_Id", "password" },
        { "loginButton_Id", "login-button" }
    };
}