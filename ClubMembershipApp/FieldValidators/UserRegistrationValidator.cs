using ClubMembershipApp.Data;
using FieldValidatorAPI;

namespace ClubMembershipApp.FieldValidators;

public class UserRegistrationValidator : IFieldValidator
{
    private const int FirstName_Min_Length = 2;
    private const int FirstName_Max_Length = 50;
    private const int LastName_Min_Length = 2;
    private const int LastName_Max_Length = 50;

    delegate bool EmailExistsDel(string emailAddress);

    private FieldValidatorDel _fieldValidatorDel = null;

    private RequiredValidDel _requiredValidDel = null;
    private StringLengthValidDel _stringLengthValidDel = null;
    private DateValidDel _dateValidDel = null;
    private PatternMatchDel _patternMatchDel = null;
    private CompareFieldsValidDel _compareFieldsValidDel = null;

    private EmailExistsDel _emailExistsDel = null;

    private string[] _fieldArray = null;
    IRegister _register = null;

    public string[] FieldArray
    {
        get
        {
            if (_fieldArray == null)
                _fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserRegistrationField)).Length];
            return _fieldArray;
        }
    }

    public FieldValidatorDel ValidatorDel => _fieldValidatorDel;

    public UserRegistrationValidator(IRegister register)
    {
        _register = register;
    }

    public void InitializeValidatorDelegates()
    {
        _fieldValidatorDel = new FieldValidatorDel(ValidField);
        _emailExistsDel = new EmailExistsDel(_register.EmailExists);

        _requiredValidDel = CommonFieldValidatorFunctions.RequiredFieldValidDel;
        _stringLengthValidDel = CommonFieldValidatorFunctions.StringLengthFieldValidDel;
        _dateValidDel = CommonFieldValidatorFunctions.DateFieldValidDel;
        _patternMatchDel = CommonFieldValidatorFunctions.PatternMatchValidDel;
        _compareFieldsValidDel = CommonFieldValidatorFunctions.FieldsCompareValidDel;
    }

    private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
    {
        fieldInvalidMessage = "";
        FieldConstants.UserRegistrationField userRegistrationField = (FieldConstants.UserRegistrationField)fieldIndex;
        switch (userRegistrationField)
        {
            case FieldConstants.UserRegistrationField.Email:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : "";
                fieldInvalidMessage =
                    (fieldInvalidMessage == "" && !_patternMatchDel(fieldValue,
                        CommonRegexValidationPatterns.Email_Address_RegEx_Pattern))
                        ? $"You must enter a valid email address{Environment.NewLine}"
                        : fieldInvalidMessage;
                fieldInvalidMessage = _emailExistsDel(fieldValue) ? $"This Email address already exists{Environment.NewLine}" : fieldInvalidMessage;
                break;
            case FieldConstants.UserRegistrationField.FirstName:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : fieldInvalidMessage;
                fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLengthValidDel(fieldValue, FirstName_Min_Length, FirstName_Max_Length))
                    ? $"{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {FirstName_Min_Length} and {FirstName_Max_Length} characters{Environment.NewLine}"
                    : fieldInvalidMessage;
                break;
            case FieldConstants.UserRegistrationField.LastName:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : fieldInvalidMessage;
                fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLengthValidDel(fieldValue, LastName_Min_Length, LastName_Max_Length))
                    ? $"{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {LastName_Min_Length} and {LastName_Max_Length} characters{Environment.NewLine}"
                    : fieldInvalidMessage;
                break;
            case FieldConstants.UserRegistrationField.Password:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : "";
                fieldInvalidMessage =
                    (fieldInvalidMessage == "" && !_patternMatchDel(fieldValue,
                        CommonRegexValidationPatterns.Strong_Password_RegEx_Pattern))
                        ? $"Your password must be between 6 to 10 characters long and should contain at least 1 uppercase character, 1 lowercase character, 1 number and 1 special character{Environment.NewLine}"
                        : fieldInvalidMessage;
                break;
            case FieldConstants.UserRegistrationField.PasswordCompare:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : "";
                fieldInvalidMessage =
                    (fieldInvalidMessage == "" && !_compareFieldsValidDel(fieldValue,
                        fieldArray[(int)FieldConstants.UserRegistrationField.Password]))
                        ? $"Your password must match{Environment.NewLine}"
                        : fieldInvalidMessage;
                break;
            case FieldConstants.UserRegistrationField.DateOfBirth:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : "";
                fieldInvalidMessage =
                    (fieldInvalidMessage == "" && !_dateValidDel(fieldValue, out DateTime validDateTime))
                        ? $"Your must enter a valid date{Environment.NewLine}"
                        : fieldInvalidMessage;
                break;
            case FieldConstants.UserRegistrationField.PhoneNumber:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : "";
                fieldInvalidMessage =
                    (fieldInvalidMessage == "" && !_patternMatchDel(fieldValue, CommonRegexValidationPatterns.Indian_Phone_RegEx_Pattern))
                        ? $"Your must enter a valid phone number{Environment.NewLine}"
                        : fieldInvalidMessage;
                break;
            case FieldConstants.UserRegistrationField.AddressFirstLine:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : "";
                break;
            case FieldConstants.UserRegistrationField.AddressSecondLine:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : "";
                break;
            case FieldConstants.UserRegistrationField.AddressCity:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : "";
                break;
            case FieldConstants.UserRegistrationField.PostCode:
                fieldInvalidMessage = (!_requiredValidDel(fieldValue))
                    ? $"You must enter value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}"
                    : "";
                fieldInvalidMessage =
                    (fieldInvalidMessage == "" && !_patternMatchDel(fieldValue, CommonRegexValidationPatterns.Indian_Pin_RegEx_Pattern))
                        ? $"Your must enter a valid pin code{Environment.NewLine}"
                        : fieldInvalidMessage;
                break;
            default:
                throw new ArgumentException("This field does not exist");
        }

        return fieldInvalidMessage == "";
    }
}