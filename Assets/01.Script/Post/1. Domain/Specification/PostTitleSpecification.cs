using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class PostTitleSpecification : ISpecification<string>
{
    public string ErrorMessage { get; private set; }

    private static readonly Regex TitleRegex = new Regex(@"^{1,50}$", RegexOptions.Compiled);

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "������ ������� �� �����ϴ�.";
            return false;
        }

        if (!TitleRegex.IsMatch(value))
        {
            ErrorMessage = "������ 1~50�� �̾�߸� �մϴ�.";
            return false;
        }
        return true;
    }
}
