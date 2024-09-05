#nullable disable
using POMexample.TestApplications.OrangeHRM.BusinessProcesses;
using POMexample.TestApplications.OrangeHRM.Utilities;
using POMexample.TestApplications.OrangeHRM.Sections;
using POMexample.TestApplications.OrangeHRM.Methods;

namespace POMexample.TestApplications.OrangeHRM.TestCases;

[TestFixture]
public class ManageTestCases
{
    [Test, Order(1)]
    public async Task Edit_Profile_Test_Case_001()
    {
        var description = "Cambiar los datos del perfil.";
        var folderpath = Evidences.GetLocalPath("OrangeHRM", this.GetType().Name, nameof(this.Edit_Profile_Test_Case_001));
        var testData = Evidences.GetLocalTestDataFile("OrangeHRM", this.GetType().Name, "Edit_Profile_Test_Case_001.xlsx");
        var report = Evidences.GenerateIndividualReport(folderpath, Guid.NewGuid(), true);
        var test = report.CreateTest(nameof(this.Edit_Profile_Test_Case_001), description);
        var dataPool = Mapper.MapData(testData);

        TestSteps.Step = 0;

        var browser = await Settings.InitializeIBrowser(BrowserDriver.Chromium, false);
        var context = await browser.NewContextAsync(new BrowserNewContextOptions { ViewportSize = ViewportSize.NoViewport });
        var page = await context.NewPageAsync();

        await Init.LaunchApplicationStep(page, report, test, Environments.TestEnvironments[TestEnvironment.SIT]);
        await Init.LoginStep(page, dataPool, report, test);
        await MenuSection.SelectMenuOptionStep(page, dataPool, report, test);
        await Manage.EditProfileStep(page, dataPool, report, test);
        await ShutOff.LogoutStep(page, report, test);

        report.Flush();

        await page.CloseAsync();
        await browser.CloseAsync();
    }

    // Utiliza este template para crear tu propio script y practicar 😊
    [Test, Order(2)]
    public async Task Name_Of_Your_Test_Case_002()
    {
        var description = "Escribe aquí una descripción.";
        var folderpath = Evidences.GetLocalPath("OrangeHRM", this.GetType().Name, nameof(this.Name_Of_Your_Test_Case_002)); // ----> Cuando cambies el nombre de tu test case aquí también cambialo.
        var testData = Evidences.GetLocalTestDataFile("OrangeHRM", this.GetType().Name, "Name_Of_Your_Test_Case_002.xlsx"); // ----> Y aquí también cambialo.
        var report = Evidences.GenerateIndividualReport(folderpath, Guid.NewGuid(), true);
        var test = report.CreateTest(nameof(this.Name_Of_Your_Test_Case_002), description); // ----> Y aquí :P
        var dataPool = Mapper.MapData(testData); // ----> Recuerda crear un archivo Excel con el nombre de tu script para poder mapear los datos.

        TestSteps.Step = 0;

        var browser = await Settings.InitializeIBrowser(BrowserDriver.Chromium, false); // ----> Puedes escoger entre las 3 opciones permitidas para "Playwright" BrowserDriver.Firefox, BrowserDriver.Chromium o BrowserDriver.Webkit.
        var context = await browser.NewContextAsync(new BrowserNewContextOptions { ViewportSize = ViewportSize.NoViewport });
        var page = await context.NewPageAsync();

        // Recuerda siempre salvar el reporte y cerrar la página y el navegador.
        report.Flush();

        await page.CloseAsync();
        await browser.CloseAsync();
    }
}