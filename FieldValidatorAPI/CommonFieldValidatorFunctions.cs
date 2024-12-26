using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;

namespace FieldValidatorAPI;

public delegate bool RequiredValidDel(string fieldVal);

public delegate bool StringLengthValidDel(string fieldVal, int minLength, int maxLength);

public delegate bool DateValidDel(string fieldVal, out DateTime validDateTime);

public delegate bool PatternMatchDel(string fieldVal, string pattern);

public delegate bool CompareFieldsValidDel(string fieldVal, string fieldValCompare);

public class CommonFieldValidatorFunctions
{
    private static RequiredValidDel _requiredValidDel = null;
    private static StringLengthValidDel _stringLengthValidDel = null;
    private static DateValidDel _dateValidDel = null;
    private static PatternMatchDel _patternMatchDel = null;
    private static CompareFieldsValidDel _compareFieldsValidDel = null;

    public static RequiredValidDel RequiredFieldValidDel
    {
        get
        {
            if (_requiredValidDel == null)
                _requiredValidDel = new RequiredValidDel(RequiredFieldValid);
            return _requiredValidDel;
        }
    }

    public static StringLengthValidDel StringLengthFieldValidDel
    {
        get
        {
            if (_stringLengthValidDel == null)
                _stringLengthValidDel = new StringLengthValidDel(StringFieldLengthValid);
            return _stringLengthValidDel;
        }
    }

    public static DateValidDel DateFieldValidDel
    {
        get
        {
            if (_dateValidDel == null)
                _dateValidDel = new DateValidDel(DateFieldValid);
            return _dateValidDel;
        }
    }

    public static PatternMatchDel PatternMatchValidDel
    {
        get
        {
            if (_patternMatchDel == null)
                _patternMatchDel = new PatternMatchDel(FieldPatternValid);
            return _patternMatchDel;
        }
    }

    public static CompareFieldsValidDel FieldsCompareValidDel
    {
        get
        {
            if (_compareFieldsValidDel == null)
                _compareFieldsValidDel = new CompareFieldsValidDel(FieldComparisonValid);
            return _compareFieldsValidDel;
        }
    }


    private static bool RequiredFieldValid(string fieldVal)
    {
        return !string.IsNullOrEmpty(fieldVal);
    }

    private static bool StringFieldLengthValid(string fieldVal, int minLength, int maxLength)
    {
        return fieldVal.Length >= minLength && fieldVal.Length <= maxLength;
    }

    private static bool DateFieldValid(string dateTime, out DateTime validDateTime)
    {
        return DateTime.TryParse(dateTime, out validDateTime);
    }

    private static bool FieldPatternValid(string fieldVal, string regexPattern)
    {
        Regex regex = new(regexPattern);
        return regex.IsMatch(fieldVal);
    }

    private static bool FieldComparisonValid(string field1, string field2)
    {
        return field1.Equals(field2);
    }
}