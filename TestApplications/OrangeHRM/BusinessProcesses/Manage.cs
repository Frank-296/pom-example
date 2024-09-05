#nullable disable
using POMexample.TestApplications.OrangeHRM.Locators;

namespace POMexample.TestApplications.OrangeHRM.BusinessProcesses;

public class Manage
{
    static Byte[] screenshot;

    public static async Task EditProfileStep(IPage page, List<DataPool> dataPool, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            // Validamos que se muestra la página de los detalles personales buscando el Label.
            var profileLabel = await page.WaitForSelectorAsync(ProfileLocators.Locators["profileLabel_Xpath"]);
            var profileLabelIsDisplayed = await profileLabel.IsVisibleAsync();

            // Si el Label se visualiza correctamente entonces continuamos con el llenado del formulario.
            Assert.That(profileLabelIsDisplayed, Is.EqualTo(true));

            // Buscamos el input para ingresar el primer nombre se lo enviamos de una vez en una sola línea.
            await page.Locator(ProfileLocators.Locators["firstNameInput_Xpath"])
                .FillAsync(dataPool.FirstOrDefault(x => x.Parameter == "FirstName").Value);

            // Hacemos lo mismo para ingresar el segundo nombre.
            await page.Locator(ProfileLocators.Locators["middleNameInput_Xpath"])
                .FillAsync(dataPool.FirstOrDefault(x => x.Parameter == "MiddleName").Value);

            // Y hacemos lo mismo para ingresar el apellido.
            await page.Locator(ProfileLocators.Locators["lastNameInput_Xpath"])
                .FillAsync(dataPool.FirstOrDefault(x => x.Parameter == "LastName").Value);

            // Buscamos el select para ingresar la nacionalidad.
            await page.Locator(ProfileLocators.Locators["nationalitySelect_Xpath"]).ClickAsync();

            // Esperamos a que aparezca la opción de la nacionalidad que indicamos en el Excel.
            var nationalityOption = await page.WaitForSelectorAsync(String.Format(ProfileLocators.Locators["nationalityOption_Xpath"], dataPool
                .FirstOrDefault(x => x.Parameter == "Nationality").Value));

            // Le damos clic a la opción.
            await nationalityOption.ClickAsync();

            // Buscamos el select para ingresar el estado civil.
            await page.Locator(ProfileLocators.Locators["maritalStatusSelect_Xpath"]).ClickAsync();

            // Esperamos a que aparezca la opción del estado civil que indicamos en el Excel.
            var maritalStatusOption = await page.WaitForSelectorAsync(String.Format(ProfileLocators.Locators["maritalStatusOption_Xpath"], dataPool
                .FirstOrDefault(x => x.Parameter == "MaritalStatus").Value));

            // Le damos clic a la opción.
            await maritalStatusOption.ClickAsync();

            var dateOfBirth = DateTime.Parse(dataPool.FirstOrDefault(x => x.Parameter == "DateOfBirth").Value);
            var month = dateOfBirth.ToString("MMMM", new CultureInfo("en-US"));

            // Buscamos el select para ingresar la fecha de nacimiento.
            await page.Locator(ProfileLocators.Locators["dateOfBirthSelect_Xpath"]).ClickAsync();

            // Buscamos el div para seleccionar el año de nacimiento.
            await page.Locator(ProfileLocators.Locators["yearOfBirthDiv_Xpath"]).ClickAsync();

            // Esperamos a que aparezca la opción del año de nacimiento que indicamos en el Excel.
            var yearOfBirthOption = await page.WaitForSelectorAsync(String.Format(ProfileLocators.Locators["yearOfBirthOption_Xpath"], dateOfBirth.Year));

            // Le damos clic a la opción.
            await yearOfBirthOption.ClickAsync();

            // Buscamos el div para seleccionar el mes de nacimiento.
            await page.Locator(ProfileLocators.Locators["monthOfBirthDiv_Xpath"]).ClickAsync();

            // Esperamos a que aparezca la opción del mes de nacimiento que indicamos en el Excel.
            var monthOfBirthOption = await page.WaitForSelectorAsync(String.Format(ProfileLocators.Locators["monthOfBirthOption_Xpath"], $"{month[0].ToString().ToUpper()}{month[1..]}"));

            // Le damos clic a la opción.
            await monthOfBirthOption.ClickAsync();

            // Esperamos a que aparezca la opción del día de nacimiento que indicamos en el Excel.
            var dayOfBirthOption = await page.WaitForSelectorAsync(String.Format(ProfileLocators.Locators["dayOfBirthOption_Xpath"], DateTime.Parse(dataPool
                .FirstOrDefault(x => x.Parameter == "DateOfBirth").Value).Day));

            // Le damos clic a la opción.
            await dayOfBirthOption.ClickAsync();

            // Seleccionamos el género.
            await page.Locator(String.Format(ProfileLocators.Locators["genderRadioButton_Xpath"], dataPool
                .FirstOrDefault(x => x.Parameter == "Gender").Value)).ClickAsync();

            // Damos clic en el botón guardar.
            await page.Locator(ProfileLocators.Locators["saveButton_Xpath"]).First.ClickAsync();

            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Pass;
            var step = TestSteps.Step + " El perfil del usuario se actualizó correctamente.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
        }
        // Si el Label no se visualiza correctamente de igual forma tomamos la captura y guardamos el log en el reporte marcando el paso como Fail.
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = await page.ScreenshotAsync();

            var status = Status.Fail;
            var step = TestSteps.Step + " La actualización del perfil del usuario falló.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshot)).Build();

            test.Log(status, step, evidence);
            report.Flush();

            await page.CloseAsync();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }
}