namespace POMexample.TestApplications.OrangeHRM.Utilities;

public class Environments
{
    public static readonly Dictionary<TestEnvironment, String> TestEnvironments = new()
    {
        { TestEnvironment.DEV, "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login" },
        { TestEnvironment.SIT, "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login" },
        { TestEnvironment.UAT, "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login" },
        { TestEnvironment.PROD, "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login" }
    };
}