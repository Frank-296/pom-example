#nullable disable
namespace POMexample.Utilities;

public class Mapper
{
    // Esta función servirá para extraer datos de un archivo Excel
    // La estructura de dicho Excel debe ser la siguiente:
    // ------------------------------------------------------
    // |     |       A      |       B       |       C       |
    // ------------------------------------------------------
    // |  1  |   VARIABLE   |     DATA      |  DESCRIPTION  |
    // ------------------------------------------------------
    // |  2  |              |               |               |
    // ------------------------------------------------------
    //
    // En la columna A debe ir el nombre de la variable para poder mapearla con la librería LINQ.
    // En la columna B debe ir el valor de la variable para extraerla con librería LINQ.
    // En la columna C debe ir una breve descripción (Opcional).
    //
    // Ejemplo:
    // ------------------------------------------------------------------------------------------
    // |     |             A            |             B             |             C             |
    // ------------------------------------------------------------------------------------------
    // |  1  |         VARIABLE         |           DATA            |        DESCRIPTION        |
    // ------------------------------------------------------------------------------------------
    // |  2  |         Username         |        standard_user      |    User name to login.    |
    // ------------------------------------------------------------------------------------------
    // |  3  |         Password         |        secret_sauce       |  User password to login.  |
    // ------------------------------------------------------------------------------------------
    //
    // Como parámetro le pasamos el archivo Excel en formato Byte[].
    public static List<DataPool> MapData(Byte[] testData)
    {
        // Convertimos el Byte[] a MemoryStream.
        var memoryStream = new MemoryStream(testData);
        // Extraemos el libro Excel del MemoryStream utilizando la librería "ClosedXML".
        var workBook = new XLWorkbook(memoryStream);
        // Obtenemos la primera hoja del libro.
        var workSheet = workBook.Worksheet(1);

        // Con la librería LINQ retornamos todos los datos extraídos de la primera hoja del libro Excel en forma de lista "List<DataPool>".
        return (from cell in workSheet.RangeUsed().RowsUsed() // ----> RangeUsed().RowsUsed() sirve para obtener todas las columnas y filas usadas del libro en este caso de A1 hasta C3.
                let dataPool = new DataPool()
                {
                    Parameter = cell.Cell(1).GetString(), // ----> cell.Cell(1).GetString() indica que asignaremos a Paremeter el valor de la celda A1 y como es iteración también el de A2 y A3.
                    Value = cell.Cell(2).GetString(), // ----> cell.Cell(2).GetString() indica que asignaremos a Value el valor de la celda B1 y como es iteración también el de B2 y B3.
                    Description = cell.Cell(3).GetString() // ----> cell.Cell(3).GetString() indica que asignaremos a Description el valor de la celda C1 y como es iteración también el de C2 y C3.
                }
                select dataPool).ToList(); // ----> Finalmente retornamos todo el resultado en forma de lista.
    }
}

public class DataPool
{
    public String Parameter { get; set; }

    public String Value { get; set; }

    public String Description { get; set; }
}