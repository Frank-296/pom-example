namespace POMexample.TestApplications.SwagLabs.Utilities;

public class Environments
{
    public static readonly Dictionary<TestEnvironment, String> TestEnvironments = new()
    {
        { TestEnvironment.DEV, "https://www.saucedemo.com/" },
        { TestEnvironment.SIT, "https://www.saucedemo.com/" },
        { TestEnvironment.UAT, "https://www.saucedemo.com/" },
        { TestEnvironment.PROD, "https://www.saucedemo.com/" }
    };
}