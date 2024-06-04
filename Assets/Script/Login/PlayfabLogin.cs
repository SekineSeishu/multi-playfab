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
    public static PlayfabLogin Instance;
    [SerializeField] private GameObject LogionButton;
    public PlayfabShop shop;
    public PlayFabInventry Inventry;
    [SerializeField] private GameObject nameInput;
    [SerializeField] private GameObject updateNameInput;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject LoginButton;
    public Player player;
    [SerializeField] private UserProfielUI userUI;
    [SerializeField] private string userRank;
    [SerializeField] private string userExp;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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
            LogionButton.SetActive(false);
            //���[�U�[������
            nameInput.SetActive(true);
            InputValueChanged();
        }
        else
        {
            //���C�����j���[�]��
            Menu.SetActive(true);
            LoginButton.SetActive(false);
            Inventry.GetCatalogData("main");
            shop.GetCatalogData("main");
            userUI.gameObject.SetActive(true);
            GetPlayerData();
        }
    }

    //���[�U�[�̏����擾����Player�ɓn��
    public void GetPlayerData()
    {
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest
            {
                
            }, result =>
            {
                player._name = result.Data["Name"].Value;
                var playerImage = PlayFabInventry.Instance.AllItems.Find(item => item.name == result.Data["Image"].Value);
                if (playerImage == null)
                {
                    throw new System.IO.FileNotFoundException("������܂���ł���");
                }
                player.icon = playerImage.icon;
                player._rank = result.Data["Rank"].Value;
                player._exp = result.Data["Exp"].Value;
                userUI.gameObject.SetActive(true);
                userUI.SetProfiel(player._name,player.icon);
            }, error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }


    //�\�����̓��̓R���g���[��
    [SerializeField] TMP_InputField inputName;
    //�����{�^��
    [SerializeField] Button inputComp;

    //���O�����͂���Ă��邩�̊m�F
    public void RegisterAccount()
    {
        string name = inputName.text;
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("Name����ł��B");
            return;
        }
        InputComplete();
    }
    //PlayFab�̃��[�U�[�A�J�E���g�̕\�����ɖ��O��n��
    public void PlayerUpdateUserTitleDisplayName(string name)
    {
        if (string.IsNullOrEmpty(inputName.text))
        {
            Debug.LogError("���͂��ꂽ���O����ł��B");
            return;
        }
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        }, result =>
        {
            Debug.Log("�v���C���[��:" + result.DisplayName);
        }, error => Debug.LogError("�v���C���[���̐ݒ�G���[: " + error.GenerateErrorReport()));
    }

    //���O�̕ύX���Ăяo��

    //���O�L�����̍X�V
    public void InputValueChanged()
    {
        inputComp.interactable = IsValidName();
    }

    //���O�̕���������
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
            /*nametext.SetActive(false);
            Inventry.GetCatalogData("main");
            Menu.SetActive(true);
            LoginButton.SetActive(false);
            shop.GetCatalogData("main");*/
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion
    //���O�̕ύX���Ăяo��
    public void PlayerNameUpdate()
    {
        updateNameInput.SetActive(true);
        InputValueChanged();
    }

    //�\�����̓��̓R���g���[��
    [SerializeField] TMP_InputField upDateInputName;
    //�����{�^��
    [SerializeField] Button updateInputComp;
    //���O�̕ύX
    public void UpdatePlayerName()
    {
        updateNameInput.SetActive(false);
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                { "Name", upDateInputName.text },
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnSuccess, OnError);

        void OnSuccess(UpdateUserDataResult result)
        {
            Debug.Log("Success");
            GetPlayerData();
            PlayerUpdateUserTitleDisplayName(upDateInputName.text);
        }
        void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }
    }

    public void PlayerUpdateUserData(string name)
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                { "Name", name },
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnSuccess, OnError);

        void OnSuccess(UpdateUserDataResult result)
        {
            Debug.Log("Success");
            GetPlayerData();
            PlayerUpdateUserTitleDisplayName(name);
        }
        void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }
    }

    #region �v���C���[�̏�����
    public void InitPlayer()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                {"Name", inputName.text},
                {"Image","���@�g��" },
                {"Exp",userExp},
                {"Rank",userRank}
            }
        };

        PlayFabClientAPI.UpdateUserData(request
            , result =>
            {
                Debug.Log("�v���C���[�̏���������");
                //UpdateUserTitleDisplayName();
                nameInput.SetActive(false);
                Inventry.GetCatalogData("main");
                Menu.SetActive(true);
                LoginButton.SetActive(false);
                shop.GetCatalogData("main");
                userUI.gameObject.SetActive(true);
                GetPlayerData(/*success.PlayFabId*/);
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion

    private void PlayFabInitPlayer()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                {"Exp","�O" },
                {"Rank","�P" },
                {"Name","" },
                {"Image","���@�g��" },
            }
        };

        PlayFabClientAPI.UpdateUserData(request
            , result =>
            {
                Debug.Log("�v���C���[�̏���������");
                nameInput.SetActive(false);
                Menu.SetActive(true);
                LoginButton.SetActive(false);
                Inventry.GetCatalogData("main");
                //shop.GetCatalogData("main");
                userUI.gameObject.SetActive(true);
                //GetPlayerData(/*success.PlayFabId*/);
                PlayerUpdateUserTitleDisplayName(inputName.text);
                PlayerUpdateUserData(inputName.text);
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    //�������̍ۂɌĂяo��
    public void InputComplete()
    {
        PlayFabInitPlayer();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.OnLogin)
        {
            LogionButton.SetActive(true);
            Menu.SetActive(false);
            GameManager.Instance.OnLogin = false;
        }
        else
        {
            LogionButton.SetActive(false);
            Menu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}