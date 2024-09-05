namespace POMexample.TestApplications.OrangeHRM.Locators;

public class ProfileLocators
{
    public static readonly Dictionary<String, String> Locators = new()
    {
        { "profileLabel_Xpath", "//h6[text()='Personal Details']" },
        { "firstNameInput_Xpath", "//input[@name='firstName']" },
        { "middleNameInput_Xpath", "//input[@name='middleName']" },
        { "lastNameInput_Xpath", "//input[@name='lastName']" },
        { "nationalitySelect_Xpath", "//label[text()='Nationality']/parent::div/following-sibling::div" },
        { "nationalityOption_Xpath", "//div[@role='option']/span[text()='{0}']" },
        { "maritalStatusSelect_Xpath", "//label[text()='Marital Status']/parent::div/following-sibling::div" },
        { "maritalStatusOption_Xpath", "//div[@role='option']/span[text()='{0}']" },
        { "dateOfBirthSelect_Xpath", "//label[text()='Date of Birth']/parent::div/following-sibling::div" },
        { "yearOfBirthDiv_Xpath", "//div[@class='oxd-calendar-selector-year-selected']" },
        { "yearOfBirthOption_Xpath", "//div[@class='oxd-calendar-selector-year-selected']/following-sibling::ul/li[text()='{0}']" },
        { "monthOfBirthDiv_Xpath", "//div[@class='oxd-calendar-selector-month-selected']" },
        { "monthOfBirthOption_Xpath", "//div[@class='oxd-calendar-selector-month-selected']/following-sibling::ul/li[text()='{0}']" },
        { "dayOfBirthOption_Xpath", "//div[@class='oxd-calendar-date'][text()='{0}']" },
        { "genderRadioButton_Xpath", "//label[text()='{0}']" },
        { "saveButton_Xpath", "//button[@type='submit']" }
    };
}