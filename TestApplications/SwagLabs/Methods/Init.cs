#nullable disable
using POMexample.TestApplications.SwagLabs.Locators;

namespace POMexample.TestApplications.SwagLabs.Methods;

// El nombre de nuestra clase, a mí me gusta usar siempre Init de Initialize.
public class Init
{
    // Creamos un objeto de tipo Screenshot de "Selenium" para tomar capturas de pantalla.
    static Screenshot screenshot;
    // Para los void a mí me gusta siempre finalizarlos con la palabra Step ya que al final de cuentas en el script los llamaremos y es fácil identificarlos como pasos,
    // es importante pasarle como parámetros el driver, el reporte y la url de la aplicación.
    public static void LaunchApplicationStep(IWebDriver driver, ExtentReports report, ExtentTest test, String environmentUrl)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        // Un bloque try { } catch { } para cachar excepciones e indicar que la ejecución del script falló.
        try
        {
            // Maximizamos el navegador.
            driver.Manage().Window.Maximize();
            // Navegamos a la url de la aplicación.
            driver.Navigate().GoToUrl(environmentUrl);

            // Validamos que la aplicación cargó satisfactoriamente.
            var loadPage = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            
            // Si la página cargó correctamente entonces tomamos la captura y guardamos el log en el reporte marcando el paso como Pass.
            if (loadPage)
            {
                Thread.Sleep(2000);

                screenshot = driver.TakeScreenshot();

                var status = Status.Pass;
                var step = TestSteps.Step + " La aplicación se inició correctamente.";
                var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

                test.Log(status, step, evidence);

                Assert.That(loadPage, Is.EqualTo(true));
            }
        }
        // Si la página no cargó correctamente de igual forma tomamos la captura y guardamos el log en el reporte marcando el paso como Fail.
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Fail;
            var step = TestSteps.Step + " El inicio de la aplicación falló.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            report.Flush();
            driver.Quit();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }

    public static void LoginStep(IWebDriver driver, List<DataPool> dataPool, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            // Buscamos el input para ingresar el username se lo enviamos de una vez en una sola línea.
            driver.FindElement(By.Id(LoginLocators.Locators["usernameInput_Id"])) // ----> En By.Id() le pasamos como parámetro el locator previamente declarado en nuestro diccionario.
                .SendKeys(dataPool.FirstOrDefault(x => x.Parameter == "Username").Value); // ----> Y de una vez le pasamos el valor extraído del Excel utilizando "LINQ".

            // Hacemos lo mismo para ingresar la contraseña.
            driver.FindElement(By.Id(LoginLocators.Locators["passwordInput_Id"]))
                .SendKeys(dataPool.FirstOrDefault(x => x.Parameter == "Password").Value);

            // Buscamos el botón para iniciar sesión y le damos click.
            driver.FindElement(By.Id(LoginLocators.Locators["loginButton_Id"])).Click();

            // Validamos que el inicio de sesión fue satisfactorio buscando el Label de la Home Page.
            var homeLabel = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(By.XPath(ProductsLocators.Locators["homeLabel_Xpath"])));

            // Si el Label se visualiza correctamente entonces tomamos la captura y guardamos el log en el reporte marcando el paso como Pass.
            if (homeLabel.Displayed)
            {
                Thread.Sleep(2000);

                screenshot = driver.TakeScreenshot();

                var status = Status.Pass;
                var step = TestSteps.Step + " El inicio de sesión fue exitoso.";
                var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

                test.Log(status, step, evidence);

                Assert.That(homeLabel.Displayed, Is.EqualTo(true));
            }
        }
        // Si el Label no se visualiza correctamente de igual forma tomamos la captura y guardamos el log en el reporte marcando el paso como Fail.
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Fail;
            var step = TestSteps.Step + " El inicio de sesión falló.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            report.Flush();
            driver.Quit();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }
}