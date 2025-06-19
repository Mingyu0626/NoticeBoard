using Firebase.Firestore;

[FirestoreData]
public class AccountDTO
{
    [FirestoreProperty]
    public string Email { get; set; }
    [FirestoreProperty]
    public string Nickname { get; set; }
    [FirestoreProperty]
    public string Password { get; set; }

    public AccountDTO()
    {

    }
    // ���� ������ (�Է°��� ����)
    public AccountDTO(string email, string nickname, string password)
    {
        Email = email;
        Nickname = nickname;
        Password = password;
    }

    // ������ ��ü ��� ������
    public AccountDTO(Account account)
    {
        Email = account.Email;
        Nickname = account.Nickname;
        Password = account.Password;
    }
}
