using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AccountManager : MonoBehaviourSingleton<AccountManager>
{
    private Account              _myAccount;

    public AccountDTO CurrentAcount => _myAccount.ToDTO();

    private AccountRepository    _repository;

    private const string SALT = "981226";

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        _repository = new AccountRepository();
    }

    public Result TryRegister(string email, string nickname, string password)
    {
        AccountSaveData saveData = _repository.Find(email);
        if (saveData != null)
        {
            return new Result(false, "�̹� ������ �̸����Դϴ�.");
        }

        // ��й�ȣ ��Ģ �˻�
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            return new Result(false, passwordSpecification.ErrorMessage);
        }

        string encryptedPassword = CryptoUtil.Encryption(password, SALT);
        Account account = new Account(email, nickname, encryptedPassword);

        // ���� ����
        _repository.Save(account.ToDTO());

        return new Result(true);
    }

    public bool TryLogin(string email, string password)
    {
        AccountSaveData saveData = _repository.Find(email);
        if (saveData == null)
        {
            return false;
        }

        if (CryptoUtil.Verify(password, saveData.Password, SALT))
        {
            _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
            return true;
        }
        
        return true;
    }
}
