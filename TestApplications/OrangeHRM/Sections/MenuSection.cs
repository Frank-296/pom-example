#nullable disable
using POMexample.TestApplications.OrangeHRM.Locators;

namespace POMexample.TestApplications.OrangeHRM.Sections;

public class MenuSection
{
    static Byte[] screenshot;

    public static async Task SelectMenuOptionStep(IPage page, List<DataPool> dataPool, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            // Validamos que se muestra la opción del menú buscando el Label.
            var menuOption = page.Locator(String.Format(MenuLocators.Locators["menuOption_Xpath"], dataPool
                .FirstOrDefault(x => x.Parameter == "MenuOption").Value));

            Assert.That(await menuOption.IsVisibleAsync(), Is.EqualTo(true));

            // Damos clic a la opción requerida del menú hamburguesa que previamente indicamos en el archivo Excel.
            await menuOption.ClickAsync();

            // Tomamos la captura y guardamos el log en el reporte marcando el paso como Pass.
            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Pass;
            var step = TestSteps.Step + " La selección de la opción del menú fue exitosa.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
        }
        // Si el Label no se visualiza correctamente de igual forma tomamos la captura y guardamos el log en el reporte marcando el paso como Fail.
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Fail;
            var step = TestSteps.Step + " La selección de la opción del menú falló.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
            report.Flush();

            await page.CloseAsync();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }
}
