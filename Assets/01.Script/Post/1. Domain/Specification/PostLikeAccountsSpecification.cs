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
            ErrorMessage = "좋아요 명단 리스트는 null이 될 수 없습니다.";
            return false;
        }

        return true;
    }
}
