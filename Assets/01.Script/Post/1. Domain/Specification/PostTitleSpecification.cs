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
            ErrorMessage = "제목은 비어있을 수 없습니다.";
            return false;
        }

        if (!TitleRegex.IsMatch(value))
        {
            ErrorMessage = "제목은 1~50자 이어야만 합니다.";
            return false;
        }
        return true;
    }
}
