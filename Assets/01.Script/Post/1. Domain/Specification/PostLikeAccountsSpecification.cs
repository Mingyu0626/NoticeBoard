using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PostLikeAccountsSpecification : ISpecification<(List<string>, string)>
{

    // �̸��� ����ǥ���� (������ RFC5322 ���)
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string ErrorMessage { get; private set; }

    public bool IsSatisfiedBy((List<string>, string) value)
    {
        if (value.Item1 == null)
        {
            ErrorMessage = "���ƿ� ��� ����Ʈ�� null�� �� �� �����ϴ�.";
            return false;
        }

        string myEmail = value.Item2;
        if (string.IsNullOrEmpty(myEmail))
        {
            ErrorMessage = "�̸����� ������� �� �����ϴ�.";
            return false;
        }

        if (!EmailRegex.IsMatch(myEmail))
        {
            ErrorMessage = "�ùٸ� �̸��� ������ �ƴմϴ�.";
            return false;
        }
        return true;
    }
}
