namespace POMexample.Utilities;

// Esta clase será la encargada de asignar el número de paso a las instrucciones de cada script.
public class TestSteps
{
    public static Int32 Step { get; set; } = 0;

    // Cada vez que se llame a este void el contador sumará un +1.
    public static void SetStepNumber() => Step++;
}