#nullable disable
using POMexample.TestApplications.SwagLabs.BusinessProcesses;
using POMexample.TestApplications.SwagLabs.Methods;

namespace POMexample.TestApplications.SwagLabs.TestCases;

// Este tag nos indica que tendremos una suite de test cases.
[TestFixture]
// El nombre de nuestra suite.
public class ProductsTestCases
{
    // Este tag nos indica que esto es un test case, con Order(N) indicamos el orden en caso de ejecutar la suite completa.
    [Test, Order(1)]
    public void Search_Product_Test_Case_001()
    {
        // Una breve descripción del test case.
        var description = "Buscar un producto.";
        // Generamos la ruta para guardar la evidencia.
        // Como primer parámetro le pasamos el nombre de la aplicación (Carpeta).
        // Como segundo parámetro el nombre de la clase (Test suite).
        // Como tercer parámetro el nombre del método (Test case).
        var folderpath = Evidences.GetLocalPath("SwagLabs", this.GetType().Name, nameof(this.Search_Product_Test_Case_001));
        // Obtenemos el archivo Excel.
        // Si esto falla es porque no existe la carpeta o el archivo y deberemos colocarlo manualmente.
        // --> POMexample
        // ----> TestApplications
        // ------> SwagLabs
        // --------> TestData
        // ----------> ProductsTestCases
        // ------------> Search_Product_Test_Case_001.xlsx
        var testData = Evidences.GetLocalTestDataFile("SwagLabs", this.GetType().Name, "Search_Product_Test_Case_001.xlsx");
        // Generamos el reporte.
        var report = Evidences.GenerateIndividualReport(folderpath, Guid.NewGuid(), true);
        // Creamos el test pasándole como parámetros el nombre y la descripción.
        var test = report.CreateTest(nameof(this.Search_Product_Test_Case_001), description);
        // Mapeamos el archivo Excel y obtenemos los datos en forma de lista.
        var dataPool = Mapper.MapData(testData);

        // Iniciamos el contador de pasos en 0.
        TestSteps.Step = 0;

        // Inicializamos el driver, para este script usaremos "Selenium" con el navegador Chrome y como segundo parámetro false para que se muestre el navegador.
        var driver = Settings.InitializeIWebDriver(BrowserDriver.Chrome, false);

        // A partir de aquí estas líneas son solo llamadas a los métodos que están en las otras carpetas, aplicando satisfactoriamente el patrón de diseño POM 😊
        Init.LaunchApplicationStep(driver, report, test, "https://www.saucedemo.com/");
        Init.LoginStep(driver, dataPool, report, test);
        Products.SelectProductStep(driver, dataPool, report, test);

        // Finalmente salvamos el reporte y cerramos el driver.
        report.Flush();
        driver.Quit();

    }

    [Test, Order(2)]
    public void Buy_Product_Test_Case_002()
    {
        var description = "Comprar un producto.";
        var folderpath = Evidences.GetLocalPath("SwagLabs", this.GetType().Name, nameof(this.Buy_Product_Test_Case_002));
        var testData = Evidences.GetLocalTestDataFile("SwagLabs", this.GetType().Name, "Buy_Product_Test_Case_002.xlsx");
        var report = Evidences.GenerateIndividualReport(folderpath, Guid.NewGuid(), false);
        var test = report.CreateTest(nameof(this.Buy_Product_Test_Case_002), description);
        var dataPool = Mapper.MapData(testData);

        TestSteps.Step = 0;

        // Inicializamos el driver, para este script usaremos "Selenium" con el navegador Edge y como segundo parámetro true para que no se muestre el navegador (headless).
        var driver = Settings.InitializeIWebDriver(BrowserDriver.Edge, true);

        Init.LaunchApplicationStep(driver, report, test, "https://www.saucedemo.com/");
        Init.LoginStep(driver, dataPool, report, test);
        Products.SelectProductStep(driver, dataPool, report, test);
        Products.AddProductToCartStep(driver, report, test);
        Products.CheckCartStep(driver, report, test);

        report.Flush();
        driver.Quit();
    }

    // Utiliza este template para crear tu propio script y practicar 😊
    [Test, Order(3)]
    public void Name_Of_Your_Test_Case_003()
    {
        var description = "Escribe aquí una descripción.";
        var folderpath = Evidences.GetLocalPath("SwagLabs", this.GetType().Name, nameof(this.Name_Of_Your_Test_Case_003)); // ----> Cuando cambies el nombre de tu test case aquí también cambialo.
        var testData = Evidences.GetLocalTestDataFile("SwagLabs", this.GetType().Name, "Name_Of_Your_Test_Case_003.xlsx"); // ----> Y aquí también cambialo.
        var report = Evidences.GenerateIndividualReport(folderpath, Guid.NewGuid(), true);
        var test = report.CreateTest(nameof(this.Name_Of_Your_Test_Case_003), description); // ----> Y aquí :P
        var dataPool = Mapper.MapData(testData); // ----> Recuerda crear un archivo Excel con el nombre de tu script para poder mapear los datos.

        TestSteps.Step = 0;

        var driver = Settings.InitializeIWebDriver(BrowserDriver.Edge, false); // ----> Puedes escoger entre las 3 opciones permitidas para "Selenium" BrowserDriver.Chrome, BrowserDriver.Edge o BrowserDriver.Firefox.

        // Recuerda siempre salvar el reporte y cerrar el driver.
        report.Flush();
        driver.Quit();
    }
}