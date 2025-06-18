using DG.Tweening;
using System;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText;                  // 결과 텍스트
    public TMP_InputField  EmailInputField;
    public TMP_InputField  NicknameInputField;
    public TMP_InputField  PasswordInputField;
    public TMP_InputField  PasswordComfirmInputField;
    public Button          ConfirmButton;                        // 로그인 or 회원가입 버튼
}

public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject     LoginPanel;
    public GameObject     RegisterPanel;

    [Header("로그인")]
    public UI_InputFields LoginInputFields;

    [Header("회원가입")]
    public UI_InputFields RegisterInputFields;

    [Header("트위닝")]
    public float          WarningShakeTime      = 1f;
    public float          WarningShakeMagnitude = 5f;

    private const string  PREFIX                = "ID_";

    private void Start()
    {
        OnClickGoToLoginButton();
    }

    public void OnClickGoToRegisterButton()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
        RegisterInputFields.ResultText.enabled = false;

        RegisterInputFields.EmailInputField.text = string.Empty;
        RegisterInputFields.PasswordInputField.text = string.Empty;
        RegisterInputFields.PasswordComfirmInputField.text = string.Empty;
    }

    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        LoginInputFields.ResultText.enabled = false;
    }

    // 회원가입
    public void Register()
    {
        // 1. 아이디 입력을 확인한다.
        string email = RegisterInputFields.EmailInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            ShowResultText(RegisterInputFields, emailSpecification.ErrorMessage);
            return;
        }

        // 2. 닉네임 입력을 확인한다.
        string nickname = RegisterInputFields.NicknameInputField.text;
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            ShowResultText(RegisterInputFields, nicknameSpecification.ErrorMessage);
            return;
        }

        // 2. 비밀번호를 입력한다.
        string password = RegisterInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            ShowResultText(RegisterInputFields, passwordSpecification.ErrorMessage);
            return;
        }

        // 3. 2차 비밀번호 입력을 확인하고, 1차 비밀번호 입력과 같은지 확인한다.
        string passwordComfirm = RegisterInputFields.PasswordComfirmInputField.text;
        if (!passwordSpecification.IsSatisfiedBy(passwordComfirm))
        {
            ShowResultText(RegisterInputFields, passwordSpecification.ErrorMessage);
            return;
        }
        
        if (passwordComfirm != password)
        {
            ShowResultText(RegisterInputFields, "패스워드와 패스워드 확인 값이 다릅니다..");
            return;
        }

        Result result = AccountManager.Instance.TryRegister(email, nickname, password);
        if (result.IsSuccess)
        {
            // 5. 로그인 창으로 돌아간다.
            // (이때 아이디는 자동 입력되어 있다.)
            LoginInputFields.EmailInputField.text = email;
            OnClickGoToLoginButton();
        }
        else
        {
            ShowResultText(RegisterInputFields, result.Message);
        }
    }

    // 로그인
    public void Login()
    {
        // 1. 이메일을 입력을 확인한다.
        string email = LoginInputFields.EmailInputField.text;

        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            ShowResultText(LoginInputFields, emailSpecification.ErrorMessage);
            return;
        }

        // 2. 비밀번호를 입력한다.
        string password = LoginInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            ShowResultText(LoginInputFields, passwordSpecification.ErrorMessage);
            return;
        }

        if (AccountManager.Instance.TryLogin(email, password))
        {
            SceneManager.LoadScene(1);
        }

        ShowResultText(LoginInputFields, "이메일과 비밀번호를 확인해 주세요.");
    }

    private void ShowResultText(UI_InputFields inputFields, string resultText)
    {
        if (!inputFields.ResultText.enabled)
        {
            inputFields.ResultText.enabled = true;
        }
        inputFields.ResultText.text = resultText;
        inputFields.ResultText.rectTransform.DOShakePosition(WarningShakeTime, WarningShakeMagnitude);
    }
}
