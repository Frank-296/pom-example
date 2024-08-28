#nullable disable
namespace POMexample.Utilities;

// Esta clase será la encargada de guardar todas las evidencias de todas las ejecuciones de los scripts.
public class Evidences
{
    // Esta función retornará un ExtentReports que básicamente es el reporte de ejecución en formato HTML.
    // Como primer parámetro le pasaremos la ruta donde se guardará el reporte por ejemplo: C:\
    // Como segundo parámetro un nombre para el reporte en este caso un identificador único Guid (XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX).
    // Como tercer parámetro un booleano donde true es para que el reporte salga con tema oscuro y false para que salga con el tema claro (Opcional).
    public static ExtentReports GenerateIndividualReport(String folderpath, Guid fileIdentifier, Boolean isDarkMode)
    {
        // Creamos el reporte pasándole como parámetros la ruta donde se guardará + el nombre del archivo + la extensión, en este caso siempre será .html.
        var sparkReporter = new ExtentSparkReporter(Path.Combine(folderpath, $"{fileIdentifier}.html"));
        var extentReport = new ExtentReports();

        // Configuramos el reporte.
        extentReport.AttachReporter(sparkReporter);
        extentReport.AddSystemInfo("OS", RuntimeInformation.OSDescription);
        extentReport.AddSystemInfo("Host name", Dns.GetHostName());
        extentReport.AddSystemInfo("Department", "QA");
        extentReport.AddSystemInfo("Username", Environment.UserName);

        // Configuramos el tema (Opcional).
        if (isDarkMode)
            sparkReporter.Config.Theme = Theme.Dark;
        else
            sparkReporter.Config.Theme = Theme.Standard;

        sparkReporter.Config.Encoding = "UTF-8";
        sparkReporter.Config.DocumentTitle = "Execution report";
        sparkReporter.Config.ReportName = "Automation report";

        return extentReport;
    }

    // Esta función retornará la ubicación donde se guardará la evidencia de cada ejecución,
    // en otras palabras el String retornado de esta función servirá como primer parámetro para la función de arriba GenerateIndividualReport().
    // Como primer parámetro le pasaremos el nombre de la aplicación que será equivalente al nombre de una carpeta previamente creada en el proyecto.
    // Como segundo parámetro el nombre de la clase el cual será la Test suite.
    // Como tercer parámetro el nombre del método el cual será el script (Test case).
    // ---------------------------------------------------
    // |  Nota: Esta función no requiere ningún cambio.  |
    // ---------------------------------------------------
    public static String GetLocalPath(String applicationName, String className, String methodName)
    {
        // Obtenemos la ruta raíz del proyecto.
        var localPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        // Creamos la ruta para la Test suite.
        // El resultado quedaría algo así:
        // C:\Users\owner\source\repos\POMexample\TestApplications\SwagLabs\Results\ProductsTestCases
        var resultsPath = Path.Combine(localPath, "POMexample", "TestApplications", applicationName, "Results", className);
        
        // Si la carpeta no existe la creamos.
        if (!Directory.Exists(resultsPath))
            Directory.CreateDirectory(resultsPath);

        // Creamos la ruta para guardar las evidencias.
        // El resultado quedaría algo así:
        // C:\Users\owner\source\repos\POMexample\TestApplications\SwagLabs\Results\ProductsTestCases\Search_Product_Test_Case_001
        var folderpath = Path.Combine(localPath, "POMexample", "TestApplications", applicationName, "Results", className, methodName);

        // Si la carpeta no existe la creamos.
        if (!Directory.Exists(folderpath))
            Directory.CreateDirectory(folderpath);

        return folderpath;
    }

    // Con esta función podremos buscar archivos Excel en el proyecto.
    // ---------------------------------------------------
    // |  Nota: Esta función no requiere ningún cambio.  |
    // ---------------------------------------------------
    public static Byte[] GetLocalTestDataFile(String applicationName, String className, String fileName)
    {
        // Obtenemos la ruta raíz del proyecto.
        var localPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        // Buscamos la carpeta de la Test data.
        // El resultado quedaría algo así:
        // C:\Users\owner\source\repos\POMexample\TestApplications\SwagLabs\TestData\ProductsTestCases
        var testDataPath = Path.Combine(localPath, "POMexample", "TestApplications", applicationName, "TestData", className);

        // Si la carpeta no existe la creamos.
        if (!Directory.Exists(testDataPath))
        {
            Directory.CreateDirectory(testDataPath);
            // Retornamos null porque como la carpeta no existía el script no se puede ejecutar.
            // Al menos llegado a este punto la carpeta ya se creó.
            return null;
        }

        // Buscamos el archivo en la carpeta.
        // El resultado quedaría algo así:
        // C:\Users\owner\source\repos\POMexample\TestApplications\SwagLabs\TestData\ProductsTestCases\Search_Product_Test_Case_001\Search_Product_Test_Case_001.xlsx
        var filePath = Path.Combine(testDataPath, fileName);

        // Si el archivo no existe debemos guardarlo manualmente.
        if (!File.Exists(filePath))
            return null;

        // Convertimos el archivo a Byte[].
        var file = File.ReadAllBytes(filePath);

        return file;
    }
}