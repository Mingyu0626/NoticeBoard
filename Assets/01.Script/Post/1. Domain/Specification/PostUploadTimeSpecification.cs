using System;
using UnityEngine;

public class PostUploadTimeSpecification : ISpecification<DateTime>
{
    public string ErrorMessage { get; private set; }

    public bool IsSatisfiedBy(DateTime value)
    {
        if (value == new DateTime())
        {
            ErrorMessage = "���ε� �ð��� �� ���� �� �� �����ϴ�.";
            return false;
        }
        return true;
    }
}
