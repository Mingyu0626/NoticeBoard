using System;
using System.Collections.Generic;
using UnityEngine;

public class PostLikeAccountsSpecification : ISpecification<List<string>>
{
    public string ErrorMessage { get; private set; }

    public bool IsSatisfiedBy(List<string> value)
    {
        if (value == null)
        {
            ErrorMessage = "���ƿ� ��� ����Ʈ�� null�� �� �� �����ϴ�.";
            return false;
        }

        return true;
    }
}
