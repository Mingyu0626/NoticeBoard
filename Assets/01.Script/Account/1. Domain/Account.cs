using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public Account(string email, string nickname, string password)
    {
        // 규칙을 객체로 캡슐화해서 분리한다.
        // 그래서 도메인과 UI는 모두 "이 규칙을 만족하니?" 물으면 된다.
        // 캡슐화한 규칙 : 명세

        // 이메일 검증
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        // 닉네임 검증
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        

        // 비밀번호 검증
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("비밀번호는 비어있을 수 없습니다.");
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, Nickname, Password);
    }
}
