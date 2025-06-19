using Firebase.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;
using System.Linq;
using Firebase.Firestore;

public class AccountRepository
{
    public const string SAVE_PREFIX = "ACCOUNT_";

    public async Task<Result> Register(AccountDTO account)
    {
        try
        {
            await FirebaseManager.Instance.Auth.CreateUserWithEmailAndPasswordAsync(account.Email, account.Password);
            await AddAcountData(account);
            return new Result(true);
        }
        catch (Exception ex)
        {
            string errorMessage = $"ȸ�����Կ� �����߽��ϴ�. {ex.Message}";

            if (ex is Firebase.FirebaseException firebaseEx)
            {
                var errorCode = (Firebase.Auth.AuthError)firebaseEx.ErrorCode;
                if (errorCode == Firebase.Auth.AuthError.EmailAlreadyInUse)
                {
                    errorMessage = "�̹� ���Ե� �̸����Դϴ�.";
                    
                }
            }

            return new Result(false, errorMessage);
        }
    }

    private async Task AddAcountData(AccountDTO account)
    {
        Debug.Log(account.Email);
        try
        {
            await FirebaseManager.Instance.Database
                .Collection("account")
                .Document(account.Email)
                .SetAsync(account);

            Debug.Log($"ȸ�� ���� ���� ���� : {account.Email}");
        }
        catch (Exception ex)
        {
            Debug.Log($"ȸ�� ���� ���� ���� : {ex.Message}");
        }
    }

    public async Task<bool> Login(AccountDTO account)
    {
        try
        {
            await FirebaseManager.Instance.Auth.SignInWithEmailAndPasswordAsync(account.Email, account.Password);
            Debug.LogFormat("�α��ο� �����߽��ϴ�. : {0} ({1})", account.Nickname, account.Email);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"�α��ο� �����߽��ϴ�. {ex.Message}");
            return false;
        }
    }

    public async Task<AccountDTO> GetAccount(string email)
    {
        try
        {
            DocumentReference docRef = FirebaseManager.Instance.Database.Collection("account").Document(email);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                AccountDTO account = snapshot.ConvertTo<AccountDTO>();
                return account;
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Account �޾ƿ��� �����߽��ϴ�.{ex.Message}");
        }
    }
}