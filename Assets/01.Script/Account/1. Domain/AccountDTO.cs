using Firebase.Firestore;

[FirestoreData]
public class AccountDTO
{
    [FirestoreProperty]
    public string Email { get; private set; }
    [FirestoreProperty]
    public string Nickname { get; private set; }
    [FirestoreProperty]
    public string Password { get; private set; }

    public AccountDTO()
    {

    }
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
