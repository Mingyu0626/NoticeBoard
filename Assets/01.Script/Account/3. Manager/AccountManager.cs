using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

    public async Task<Result> TryRegister(string email, string nickname, string password)
    {
        // 비밀번호 규칙 검사
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            return new Result(false, passwordSpecification.ErrorMessage);
        }

        string encryptedPassword = CryptoUtil.Encryption(password, SALT);
        Account account = new Account(email, nickname, encryptedPassword);

        // 레포 저장
        Result result = await _repository.Register(account.ToDTO());

        return result;
    }

    public async Task<bool> TryLogin(string email, string password)
    {
        if (!await _repository.IsAccountExists(email))
        {
            return false;
        }

        if (await _repository.Login(new AccountDTO(email, "", password)))
        {
            AccountDTO saveData = await _repository.GetAccount(email);
            if (saveData != null && CryptoUtil.Verify(password, saveData.Password, SALT))
            {
                _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
                return true;
            }
        }
        
        return false;
    }
}
