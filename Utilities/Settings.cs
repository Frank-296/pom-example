#nullable disable
namespace POMexample.Utilities;

// Esta clase será la encargada de inicializar el IWebDriver en caso de ser un script codificado con la librería de "Selenium"
// o el IBrowser en caso de ser un script codificado con la librería de "Playwright".
public class Settings
{
    // Esta función retornará un IWebDriver de "Selenium" dependiendo de la opción que le pasemos como primer parámetro el cual es un enum BrowserDriver
    // Las opciones permitidas son:
    // Chrome = 1
    // Edge = 2
    // Firefox = 3 
    // El segundo parámetro es un booleano donde false es para ejecutar el script mostrando el navegador y true es para ejecutar el script sin mostrar el navegador (headless).
    // ---------------------------------------------------
    // |  Nota: Esta función no requiere ningún cambio.  |
    // ---------------------------------------------------
    public static IWebDriver InitializeIWebDriver(BrowserDriver browser, Boolean headless)
    {
        // Un bloque try {} catch {} para cachar excepciones.
        try
        {
            // Un switch () { case: break; } para inicializar el IWebDriver dependiendo de la opción que le hemos pasado como primer parámetro a la función.
            switch (browser)
            {
                case BrowserDriver.Chrome:
                    var chromeOptions = new ChromeOptions();

                    if (headless)
                        chromeOptions.AddArgument("--headless=new");

                    return new ChromeDriver(chromeOptions);

                case BrowserDriver.Edge:
                    var edgeOptions = new EdgeOptions();

                    if (headless)
                        edgeOptions.AddArgument("--headless=new");

                    return new EdgeDriver(edgeOptions);

                case BrowserDriver.Firefox:
                    var firefoxOptions = new FirefoxOptions();

                    if (headless)
                        firefoxOptions.AddArgument("--headless=new");

                    return new FirefoxDriver(firefoxOptions);

                default:
                    throw new NotImplementedException();
            }
        }
        catch (Exception)
        {
            // Si no se le pasa como parámetro una opción válida entonces retornamos null impidiendo ejecutar el script porque no se pudo inicializar el IWebDriver.
            return null;
        }
    }

    // Esta función retornará un IBrowser de "Playwright" dependiendo de la opción que le pasemos como primer parámetro el cual es un enum BrowserDriver
    // Las opciones permitidas son:
    // Firefox = 3
    // Chromium = 4
    // Webkit = 5 
    // El segundo parámetro es un booleano donde "false" es para ejecutar el script mostrando el navegador y "true" es para ejecutar el script sin mostrar el navegador (headless).
    // ---------------------------------------------------
    // |  Nota: Esta función no requiere ningún cambio.  |
    // ---------------------------------------------------
    public static async Task<IBrowser> InitializeIBrowser(BrowserDriver browser, Boolean headless)
    {
        var playwright = await Playwright.CreateAsync();

        // Un bloque try {} catch {} para cachar excepciones.
        try
        {
            // Un switch () { case: break; } para inicializar el IBrowser dependiendo de la opción que le hemos pasado como primer parámetro a la función.
            return browser switch
            {
                BrowserDriver.Chromium => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless, Args = ["--start-maximized"] }),
                BrowserDriver.Webkit => await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless, Args = ["--start-maximized"] }),
                BrowserDriver.Firefox => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless, Args = ["--start-maximized"] }),
                _ => throw new NotImplementedException()
            };
        }
        catch (Exception)
        {
            // Si no se le pasa como parámetro una opción válida entonces retornamos null impidiendo ejecutar el script porque no se pudo inicializar el IBrowser.
            return null;
        }
    }
}

// Este es el enum con las opciones permitidas para inicializar el IWebDriver en caso de ser un script codificado con la librería de "Selenium" de la opción 1 a 3
// o el IBrowser en caso de ser un script codificado con la librería de "Playwright" de la opción 3 a 5.
public enum BrowserDriver : Int32
{
    [Display(Name = "Google Chrome")]
    Chrome = 1,
    [Display(Name = "Microsoft Edge")]
    Edge = 2,
    [Display(Name = "Mozila Firefox")]
    Firefox = 3,
    [Display(Name = "Chromium")]
    Chromium = 4,
    [Display(Name = "Webkit")]
    Webkit = 5
}