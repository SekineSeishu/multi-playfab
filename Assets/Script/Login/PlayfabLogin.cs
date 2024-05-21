using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine.UI;

public class PlayfabLogin : MonoBehaviour
{
    public PlayfabShop shop;
    public PlayFabInventry Inventry;
    [SerializeField] private GameObject nametext;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject LoginButton;

    public void Login()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }

    private void PlayFabAuthService_OnLoginSuccess(LoginResult success)
    {
        //�V�K�쐬�������ǂ���
        if (success.NewlyCreated)
        {
            //���[�U�[������
            nametext.SetActive(true);
            InputValueChanged();
        }
        else
        {
            //���C�����j���[�]��
            Menu.SetActive(true);
            LoginButton.SetActive(false);
            Inventry.GetCatalogData("main");
            shop.GetCatalogData("main");
        }
    }

    //�\�����̓��̓R���g���[��
    [SerializeField] TMP_InputField inputName;
    //�����{�^��
    [SerializeField] Button inputComp;

    public void InputValueChanged()
    {
        inputComp.interactable = IsValidName();
    }

    private bool IsValidName()
    {
        //�\������3�����ȏ�P�O�����ȓ�
        return !string.IsNullOrWhiteSpace(inputName.text)
            && 3 <= inputName.text.Length
            && inputName.text.Length <= 10;
    }
    #region �v���C���[�\���̍X�V
    private void UpdateUserTitleDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputName.text
        }, result =>
        {
            Debug.Log("�v���C���[��:" + result.DisplayName);
            nametext.SetActive(false);
            Inventry.GetCatalogData("main");
            Menu.SetActive(true);
            LoginButton.SetActive(false);
            shop.GetCatalogData("main");
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion

    #region �v���C���[�̏�����
    public void InitPlayer()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                {"Exp","0"},
                {"Rank","1"}
            }
        };

        PlayFabClientAPI.UpdateUserData(request
            , result =>
            {
                Debug.Log("�v���C���[�̏���������");
                UpdateUserTitleDisplayName();
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        nametext.SetActive(false);
        /*var request = new LoginWithCustomIDRequest
        {
            CustomId = "sample-custom-id",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                Debug.Log("���O�C������:" + result);
                shop.GetCatalogData("main");
                shop.GetStoreData("main", "gold_store");
            },
            error =>
            {
                Debug.Log("���O�C�����s:" + error);
            });*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}