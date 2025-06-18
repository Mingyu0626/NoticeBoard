public class AccountDTO
{
    public string Email;
    public string Nickname;
    public string Password;

    // 간단 생성자 (입력값만 받음)
    public AccountDTO(string email, string nickname, string password)
    {
        Email = email;
        Nickname = nickname;
        Password = password;
    }

    // 도메인 객체 기반 생성자
    public AccountDTO(Account account)
    {
        Email = account.Email;
        Nickname = account.Nickname;
        Password = account.Password;
    }
}
