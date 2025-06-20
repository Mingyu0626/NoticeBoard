using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PostLikeAccountsSpecification : ISpecification<(List<string>, string)>
{

    // 이메일 정규표현식 (간단한 RFC5322 기반)
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string ErrorMessage { get; private set; }

    public bool IsSatisfiedBy((List<string>, string) value)
    {
        if (value.Item1 == null)
        {
            ErrorMessage = "좋아요 명단 리스트는 null이 될 수 없습니다.";
            return false;
        }

        string myEmail = value.Item2;
        if (string.IsNullOrEmpty(myEmail))
        {
            ErrorMessage = "이메일은 비어있을 수 없습니다.";
            return false;
        }

        if (!EmailRegex.IsMatch(myEmail))
        {
            ErrorMessage = "올바른 이메일 형식이 아닙니다.";
            return false;
        }
        return true;
    }
}
