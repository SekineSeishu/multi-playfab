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

    //�����̃A�J�E���g�̌���
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
        Debug.Log("���O�C������");
    }

    void OnLoginError(PlayFabError error)
    {
        Debug.Log("���O�C���G���[");
        //�V�K�A�J�E���g�̍쐬
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
            Debug.LogError("���O�C�����s");
        }
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("�A�J�E���g�쐬�I");
    }

    void OnRegisterError(PlayFabError error)
    {
        Debug.LogError("�A�J�E���g�쐬���s");
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
