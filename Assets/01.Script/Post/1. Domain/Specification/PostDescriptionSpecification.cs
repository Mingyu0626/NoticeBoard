using UnityEngine;

public class PostDescriptionSpecification : ISpecification<string>
{
    public string ErrorMessage { get; private set; }

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "������ ������� �� �����ϴ�.";
            return false;
        }
        return true;
    }
}
