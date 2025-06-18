using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class AccountNicknameSpecification : ISpecification<string>
{
    // ´Ğ³×ÀÓ: ÇÑ±Û ¶Ç´Â ¿µ¾î·Î ±¸¼º, 2~7ÀÚ
    private static readonly Regex NicknameRegex = new Regex(@"^[°¡-ÆRa-zA-Z]{2,7}$", RegexOptions.Compiled);
    // ±İÁöµÈ ´Ğ³×ÀÓ (ºñ¼Ó¾î µî)
    private static readonly string[] ForbiddenNicknames = { "¹Ùº¸", "¸ÛÃ»ÀÌ", "¿î¿µÀÚ", "±èÈ«ÀÏ" };

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "´Ğ³×ÀÓÀº ºñ¾îÀÖÀ» ¼ö ¾ø½À´Ï´Ù.";
            return false;
        }

        if (!NicknameRegex.IsMatch(value))
        {
            ErrorMessage = "´Ğ³×ÀÓÀº 2ÀÚ ÀÌ»ó 7ÀÚ ÀÌÇÏÀÇ ÇÑ±Û ¶Ç´Â ¿µ¹®ÀÌ¾î¾ß ÇÕ´Ï´Ù.";
            return false;
        }

        foreach (var forbidden in ForbiddenNicknames)
        {
            if (value.Contains(forbidden))
            {
                ErrorMessage = $"´Ğ³×ÀÓ¿¡ ºÎÀûÀıÇÑ ´Ü¾î°¡ Æ÷ÇÔµÇ¾î ÀÖ½À´Ï´Ù: {forbidden}";
                return false;
            }
        }

        return true;
    }

    public string ErrorMessage { get; private set; }
}
