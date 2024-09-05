#nullable disable
using POMexample.TestApplications.OrangeHRM.Locators;

namespace POMexample.TestApplications.OrangeHRM.Methods;

public class Init
{
    // Creamos un objeto de tipo Byte[] para tomar capturas de pantalla y guardarlas con "Playwright".
    static Byte[] screenshot;

    public static async Task LaunchApplicationStep(IPage page, ExtentReports report, ExtentTest test, String environmentUrl)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            var loadPage = await page.GotoAsync(environmentUrl);

            // Si la página cargó correctamente entonces tomamos la captura y guardamos el log en el reporte marcando el paso como Pass.
            Assert.That(loadPage.Ok, Is.EqualTo(true));
            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Pass;
            var step = TestSteps.Step + " La aplicación se inició correctamente.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
        }
        // Si la página no cargó correctamente de igual forma tomamos la captura y guardamos el log en el reporte marcando el paso como Fail.
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Fail;
            var step = TestSteps.Step + " El inicio de la aplicación falló.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
            report.Flush();

            await page.CloseAsync();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }

    public static async Task LoginStep(IPage page, List<DataPool> dataPool, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            // Buscamos el input para ingresar el username se lo enviamos de una vez en una sola línea.
            await page.Locator(LoginLocators.Locators["usernameInput_Xpath"])
                .FillAsync(dataPool.FirstOrDefault(x => x.Parameter == "Username").Value);

            // Hacemos lo mismo para ingresar la contraseña.
            await page.Locator(LoginLocators.Locators["passwordInput_Xpath"])
                .FillAsync(dataPool.FirstOrDefault(x => x.Parameter == "Password").Value);

            // Buscamos el botón para iniciar sesión y le damos click.
            await page.Locator(LoginLocators.Locators["loginButton_Xpath"]).ClickAsync();

            // Validamos que el inicio de sesión fue satisfactorio buscando el Label de la Home Page.
            var homeLabel = await page.WaitForSelectorAsync(HomeLocators.Locators["homeLabel_Xpath"]);
            var homeLabelIsDisplayed = await homeLabel.IsVisibleAsync();

            // Si el Label se visualiza correctamente entonces tomamos la captura y guardamos el log en el reporte marcando el paso como Pass.
            Assert.That(homeLabelIsDisplayed, Is.EqualTo(true));
            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Pass;
            var step = TestSteps.Step + " El inicio de sesión fue exitoso.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
        }
        // Si el Label no se visualiza correctamente de igual forma tomamos la captura y guardamos el log en el reporte marcando el paso como Fail.
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Fail;
            var step = TestSteps.Step + " El inicio de sesión falló.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
            report.Flush();

            await page.CloseAsync();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }
}