#nullable disable
using POMexample.TestApplications.SwagLabs.Locators;

namespace POMexample.TestApplications.SwagLabs.Methods;

public class ShutOff
{
    static Screenshot screenshot;

    public static void LogoutStep(IWebDriver driver, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            driver.FindElement(By.Id(MenuLocators.Locators["menuButton_Id"])).Click();

            Thread.Sleep(1000);

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(By.Id(MenuLocators.Locators["logoutLink_Id"]))).Click();
            
            var loginButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(By.Id(LoginLocators.Locators["loginButton_Id"])));
            
            Assert.That(loginButton.Displayed, Is.EqualTo(true));
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Pass;
            var step = TestSteps.Step + " El cierre de sesión fue exitoso.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);
        }
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Fail;
            var step = TestSteps.Step + " El cierre de sesión falló.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            report.Flush();
            driver.Quit();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }
}