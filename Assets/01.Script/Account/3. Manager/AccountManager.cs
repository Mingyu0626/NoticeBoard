using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    private Account              _myAccount;

    public AccountDTO CurrentAcount => _myAccount.ToDTO();

    private AccountRepository    _repository;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        _repository = new AccountRepository();
    }

    private const string SALT = "981226";

    public Result TryRegister(string email, string nickname, string password)
    {
        AccountSaveData saveData = _repository.Find(email);
        if (saveData != null)
        {
            return new Result(false, "이미 가입한 이메일입니다.");
        }

        // 비밀번호 규칙 검사

        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            return new Result(false, passwordSpecification.ErrorMessage);
        }

        string encryptedPassword = CryptoUtil.Encryption(password, SALT);
        Account account = new Account(email, nickname, encryptedPassword);

        // 레포 저장
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
