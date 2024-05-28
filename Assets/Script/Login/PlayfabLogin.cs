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
    [SerializeField] private Player player;
    [SerializeField] private UserProfielUI userUiI;

    public void Login()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }

    private void PlayFabAuthService_OnLoginSuccess(LoginResult success)
    {
        Debug.Log("���O�C������");
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
            GetPlayerData(/*success.PlayFabId*/);
        }
    }

    public void GetPlayerData(/*string playfabId*/)
    {
        /*PlayFabClientAPI.GetPlayerProfile(
            new GetPlayerProfileRequest
            {
                PlayFabId = playfabId,
                ProfileConstraints = new PlayerProfileViewConstraints
                {
                    ShowDisplayName = true
                }
            }, result =>
            {
                player._name = result.PlayerProfile.DisplayName;
                Debug.Log("DisplayName:" + player.name);
            },error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });*/
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest
            {
                //PlayFabId = playfabId
            }, result =>
            {
                player._name = result.Data["Name"].Value;
                var playerImage = PlayFabInventry.Instance.AllItems.Find(item => item.name == result.Data["Image"].Value);
                if (playerImage == null)
                {
                    throw new System.IO.FileNotFoundException("������܂���ł���");
                }
                player.icon = playerImage.icon;
                userUiI.SetProfiel(player._name,player.icon);
            }, error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }


    //�\�����̓��̓R���g���[��
    [SerializeField] TMP_InputField inputName;
    //�����{�^��
    [SerializeField] Button inputComp;

    private void PlayerUpdateUserTitleDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputName.text
        }, result =>
        {
            Debug.Log("�v���C���[��:" + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    private void PlayerUpdateUserData()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                { "Name", inputName.text },
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnSuccess, OnError);

        void OnSuccess(UpdateUserDataResult result)
        {
            Debug.Log("Success");
            GetPlayerData();
        }
        void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }
    }
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
                //UpdateUserTitleDisplayName();
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion

    private void PlayFabInitPlayer()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                {"Exp","0" },
                {"Rank","1" }
            }
        };

        PlayFabClientAPI.UpdateUserData(request
            , result =>
            {
                Debug.Log("�v���C���[�̏���������");
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    private void InputComplete()
    {
        PlayFabInitPlayer();

        PlayerUpdateUserTitleDisplayName();
    }

    /*private void OnEnable()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += PlayFabAuthService_OnPlayFabError;
    }

    private void OnDisable()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError -= PlayFabAuthService_OnPlayFabError;
    }

    private void PlayFabAuthService_OnPlayFabError(PlayFabError error)
    {
        Debug.Log("���O�C�����s");
    }*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}