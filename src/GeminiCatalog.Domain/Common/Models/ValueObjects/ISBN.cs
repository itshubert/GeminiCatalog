using ErrorOr;

namespace BreadcrumbPostgres.Domain.Common.Models.ValueObjects;

public sealed record ISBN
{
    private ISBN(string value) => Value = value;
    
    public string Value { get; }

    public bool IsISBN10 => Value.Replace("-", "").Length == 10;
    public bool IsISBN13 => Value.Replace("-", "").Length == 13;

    public static ErrorOr<ISBN> Create(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return Error.Validation("ISBN.Empty", "ISBN cannot be empty");

        var cleanIsbn = isbn.Replace("-", "").Replace(" ", "");

        if (cleanIsbn.Length != 10 && cleanIsbn.Length != 13)
            return Error.Validation("ISBN.InvalidLength", "ISBN must be 10 or 13 digits");

        if (!cleanIsbn.All(char.IsDigit))
            return Error.Validation("ISBN.InvalidFormat", "ISBN must contain only digits and hyphens");

        if (!IsValidChecksum(cleanIsbn))
            return Error.Validation("ISBN.InvalidChecksum", "Invalid ISBN checksum");

        return new ISBN(FormatISBN(cleanIsbn));
    }

    public string ToBarcode() => Value.Replace("-", "");

    public string ToDisplayFormat()
    {
        var clean = ToBarcode();
        return IsISBN13 
            ? $"{clean.Substring(0, 3)}-{clean.Substring(3, 1)}-{clean.Substring(4, 4)}-{clean.Substring(8, 4)}-{clean.Substring(12, 1)}"
            : $"{clean.Substring(0, 1)}-{clean.Substring(1, 4)}-{clean.Substring(5, 4)}-{clean.Substring(9, 1)}";
    }

    private static bool IsValidChecksum(string isbn)
    {
        if (isbn.Length == 10)
            return IsValidISBN10Checksum(isbn);
        else if (isbn.Length == 13)
            return IsValidISBN13Checksum(isbn);
        
        return false;
    }

    private static bool IsValidISBN10Checksum(string isbn)
    {
        var sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += (i + 1) * int.Parse(isbn[i].ToString());
        }
        
        var checkDigit = sum % 11;
        var expectedCheckDigit = isbn[9] == 'X' ? 10 : int.Parse(isbn[9].ToString());
        
        return checkDigit == expectedCheckDigit;
    }

    private static bool IsValidISBN13Checksum(string isbn)
    {
        var sum = 0;
        for (int i = 0; i < 12; i++)
        {
            var digit = int.Parse(isbn[i].ToString());
            sum += i % 2 == 0 ? digit : digit * 3;
        }
        
        var checkDigit = (10 - (sum % 10)) % 10;
        var expectedCheckDigit = int.Parse(isbn[12].ToString());
        
        return checkDigit == expectedCheckDigit;
    }

    private static string FormatISBN(string cleanIsbn)
    {
        return cleanIsbn.Length == 13 
            ? $"{cleanIsbn.Substring(0, 3)}-{cleanIsbn.Substring(3, 1)}-{cleanIsbn.Substring(4, 4)}-{cleanIsbn.Substring(8, 4)}-{cleanIsbn.Substring(12, 1)}"
            : $"{cleanIsbn.Substring(0, 1)}-{cleanIsbn.Substring(1, 4)}-{cleanIsbn.Substring(5, 4)}-{cleanIsbn.Substring(9, 1)}";
    }
}