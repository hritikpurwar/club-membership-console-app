namespace FieldValidatorAPI;

public static class CommonRegexValidationPatterns
{
    public const string Indian_Phone_RegEx_Pattern = @"^[6-9]{1}[0-9]{9}$";
    public const string Indian_Pin_RegEx_Pattern = @"^[1-9][0-9][0-9][0-9][0-9][0-9]$";
    public const string Email_Address_RegEx_Pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
    public const string Strong_Password_RegEx_Pattern = @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,10})";
}