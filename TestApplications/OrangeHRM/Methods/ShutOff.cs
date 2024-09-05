#nullable disable
using POMexample.TestApplications.OrangeHRM.Locators;

namespace POMexample.TestApplications.OrangeHRM.Methods;

public class ShutOff
{
    static Byte[] screenshot;

    public static async Task LogoutStep(IPage page, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            await page.Locator(HomeLocators.Locators["userDropdown_Xpath"]).ClickAsync();
            await page.Locator(HomeLocators.Locators["logoutLink_Xpath"]).ClickAsync();

            var loginButton = await page.WaitForSelectorAsync(LoginLocators.Locators["loginButton_Xpath"]);
            var loginButtonIsDisplayed = await loginButton.IsVisibleAsync();

            Assert.That(loginButtonIsDisplayed, Is.EqualTo(true));
            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Pass;
            var step = TestSteps.Step + " El cierre de sesión fue exitoso.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
        }
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Fail;
            var step = TestSteps.Step + " El cierre de sesión falló.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
            report.Flush();

            await page.CloseAsync();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }
}