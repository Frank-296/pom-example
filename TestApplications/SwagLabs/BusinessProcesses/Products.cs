#nullable disable
using POMexample.TestApplications.SwagLabs.Locators;

namespace POMexample.TestApplications.SwagLabs.BusinessProcesses;

public class Products
{
    static Screenshot screenshot;

    public static void SelectProductStep(IWebDriver driver, List<DataPool> dataPool, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            // Con el locator previamente declarado en nuestro diccionario sustituimos el {0} con el nombre del producto extrayéndolo del archivo Excel.
            var productOption = driver.FindElement(By.XPath(String.Format(ProductsLocators.Locators["productOption_Xpath"], dataPool
                .FirstOrDefault(x => x.Parameter == "ProductToSearch").Value)));

            if (productOption.Displayed)
            {
                productOption.Click();

                // Finalmente solo validamos que se muestre la página con la descripción del producto, bastará con encontrar el nombre del producto.
                var productDescription = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                    .Until(d => d.FindElement(By.XPath(String.Format(ProductsLocators.Locators["productName_Xpath"], dataPool
                        .FirstOrDefault(x => x.Parameter == "ProductToSearch").Value))));

                Assert.That(productDescription.Displayed, Is.EqualTo(true));
                Thread.Sleep(2000);

                screenshot = driver.TakeScreenshot();

                var status = Status.Pass;
                var step = TestSteps.Step + " La selección del producto fue exitosa.";
                var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

                test.Log(status, step, evidence);
            }
        }
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Fail;
            var step = TestSteps.Step + " La selección del producto falló.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            report.Flush();
            driver.Quit();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }

    public static void AddProductToCartStep(IWebDriver driver, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            driver.FindElement(By.Id(ProductsLocators.Locators["addToCartButton_Id"])).Click();

            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Pass;
            var step = TestSteps.Step + " Se agregó el producto al carrito exitosamente.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);
        }
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Fail;
            var step = TestSteps.Step + " Error al agregar el producto al carrito.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            report.Flush();
            driver.Quit();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }

    public static void CheckCartStep(IWebDriver driver, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            driver.FindElement(By.Id(CartLocators.Locators["cartLink_Id"])).Click();

            // Validamos que se muestra la página del carrito.
            var cartLabel = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(By.XPath(CartLocators.Locators["cartLabel_Xpath"])));

            // Si el Label se visualiza correctamente entonces tomamos la captura y guardamos el log en el reporte marcando el paso como Pass.
            Assert.That(cartLabel.Displayed, Is.EqualTo(true));
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Pass;
            var step = TestSteps.Step + " Se visualizó el contenido del carrito exitosamente.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);
        }
        // Si el Label no se visualiza correctamente de igual forma tomamos la captura y guardamos el log en el reporte marcando el paso como Fail.
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Fail;
            var step = TestSteps.Step + " Error al visualizar el contenido del carrito.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            report.Flush();
            driver.Quit();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }

    public static void CheckoutStep(IWebDriver driver, List<DataPool> dataPool, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            driver.FindElement(By.Id(CartLocators.Locators["checkoutButton_Id"])).Click();

            // Validamos que se muestra la página del checkout.
            var checkoutLabel = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(By.XPath(CheckoutLocators.Locators["checkoutLabel_Xpath"])));

            // Si el Label se visualiza correctamente entonces llenamos el formulario tomamos la captura y guardamos el log en el reporte marcando el paso como Pass.
            Assert.That(checkoutLabel.Displayed, Is.EqualTo(true));

            driver.FindElement(By.Id(CheckoutLocators.Locators["firstNameInput_Id"]))
                .SendKeys(dataPool.FirstOrDefault(x => x.Parameter == "FirstName").Value);

            driver.FindElement(By.Id(CheckoutLocators.Locators["lastNameInput_Id"]))
                .SendKeys(dataPool.FirstOrDefault(x => x.Parameter == "LastName").Value);

            driver.FindElement(By.Id(CheckoutLocators.Locators["postalCodeInput_Id"]))
                .SendKeys(dataPool.FirstOrDefault(x => x.Parameter == "PostalCode").Value);

            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Pass;
            var step = TestSteps.Step + " Se llenó el formulario del checkout exitosamente.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            // Ejecutamos el paso para revisar la orden de compra.
            ReviewPurchaseOrderStep(driver, dataPool, report, test);
        }
        // Si el Label no se visualiza correctamente de igual forma tomamos la captura y guardamos el log en el reporte marcando el paso como Fail.
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Fail;
            var step = TestSteps.Step + " Error al llenar el formulario del checkout.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            report.Flush();
            driver.Quit();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }

    public static void ReviewPurchaseOrderStep(IWebDriver driver, List<DataPool> dataPool, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            driver.FindElement(By.Id(CheckoutLocators.Locators["continueButton_Id"])).Click();

            // Validamos que se muestra la página de la orden de compra.
            var overviewLabel = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(By.XPath(CheckoutLocators.Locators["overviewLabel_Xpath"])));

            // Si el Label se visualiza correctamente entonces validamos que el producto concuerde con el que escribimos en el archivo Excel y guardamos el log en el reporte marcando el paso como Pass.
            if (overviewLabel.Displayed)
            {
                var productName = driver.FindElement(By.XPath(String.Format(CheckoutLocators.Locators["productName_Xpath"], dataPool
                    .FirstOrDefault(x => x.Parameter == "ProductToSearch").Value)));

                Assert.That(productName.Text, Is.EqualTo(dataPool.FirstOrDefault(x => x.Parameter == "ProductToSearch").Value));
                Thread.Sleep(2000);

                screenshot = driver.TakeScreenshot();

                var status = Status.Pass;
                var step = TestSteps.Step + " Se validó la orden de compra exitosamente.";
                var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

                test.Log(status, step, evidence);

                // Ejecutamos el paso para finalizar la compra.
                FinishPurchaseStep(driver, report, test);
            }
        }
        // Si el Label no se visualiza correctamente de igual forma tomamos la captura y guardamos el log en el reporte marcando el paso como Fail.
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Fail;
            var step = TestSteps.Step + " Error al validar la orden de compra.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            report.Flush();
            driver.Quit();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }

    public static void FinishPurchaseStep(IWebDriver driver, ExtentReports report, ExtentTest test)
    {
        // Sumamos un +1 al contador de pasos.
        TestSteps.SetStepNumber();

        try
        {
            driver.FindElement(By.Id(CheckoutLocators.Locators["finishButton_Id"])).Click();

            var purchaseLabel = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(By.XPath(CheckoutLocators.Locators["purchaseLabel_Xpath"])));

            Assert.That(purchaseLabel.Displayed, Is.EqualTo(true));
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Pass;
            var step = TestSteps.Step + " La compra se finalizó exitosamente.";
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);
        }
        catch (Exception exception)
        {
            Thread.Sleep(2000);

            screenshot = driver.TakeScreenshot();

            var status = Status.Fail;
            var step = TestSteps.Step + " Error al finalizar la compra.\n" + exception.Message;
            var evidence = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build();

            test.Log(status, step, evidence);

            report.Flush();
            driver.Quit();

            TestSteps.Step = 0;
            Assert.Fail(exception.Message);
        }
    }
}