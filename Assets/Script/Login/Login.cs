using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField userNameInput;
    public TMP_InputField passwordInput;

    //既存のアカウントの検索
    public void RegisterOrLoginButton()
    {
        RegisterPlayFabUserRequest registerRequest = new RegisterPlayFabUserRequest()
        {
            Username = userNameInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false,
        };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("ログイン成功");
    }

    void OnLoginError(PlayFabError error)
    {
        Debug.Log("ログインエラー");
        //新規アカウントの作成
        if (error.Error == PlayFabErrorCode.AccountNotFound || error.Error == PlayFabErrorCode.InvalidUsernameOrPassword)
        {
            RegisterPlayFabUserRequest registerRequest = new RegisterPlayFabUserRequest()
            {
                Username = userNameInput.text,
                Password = passwordInput.text,
                RequireBothUsernameAndEmail = false,
            };
            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterError);
        }
        else
        {
            Debug.LogError("ログイン失敗");
        }
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("アカウント作成！");
    }

    void OnRegisterError(PlayFabError error)
    {
        Debug.LogError("アカウント作成失敗");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
