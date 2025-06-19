using System;
using UnityEngine;

public class PostUploadTimeSpecification : ISpecification<DateTime>
{
    public string ErrorMessage { get; private set; }

    public bool IsSatisfiedBy(DateTime value)
    {
        if (value == new DateTime())
        {
            ErrorMessage = "업로드 시간은 빈 값이 올 수 없습니다.";
            return false;
        }
        return true;
    }
}
