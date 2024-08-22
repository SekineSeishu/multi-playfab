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
    public PlayfabShop shop;//�V���b�v���̎擾
    public PlayFabInventry Inventry;//�C���x���g�����̎擾
    [SerializeField] private GameObject nameInput;//���[�U�[�����͉��
    [SerializeField] private GameObject Menu;//���j���[���
    [SerializeField] private GameObject LoginButton;//���O�C���{�^��
    public Player player;
    [SerializeField] private UserProfielUI userUI;//���[�U�[����UI
    [SerializeField] private string userRank;//�v���C���[�̃����N
    [SerializeField] private string userExp;//�v���C���[�̌o���l

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Login()
    {
        //��x�ł����O�C�����Ă����炻�̂܂܃��O�C��
        PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }

    private void PlayFabAuthService_OnLoginSuccess(LoginResult success)
    {
        Debug.Log("���O�C������");
        //�V�K�쐬�������ǂ���
        if (success.NewlyCreated)
        {
            LoginButton.SetActive(false);
            //���[�U�[������
            nameInput.SetActive(true);
            InputValueChanged();
        }
        else
        {
            //���C�����j���[�]��
            //�C���x���g���A�V���b�v�̎擾
            Menu.SetActive(true);
            LoginButton.SetActive(false);
            Inventry.GetCatalogData("main");
            shop.GetCatalogData("main");
            userUI.gameObject.SetActive(true);
            GetPlayerData();
        }
    }

    //���[�U�[�̏��擾
    public void GetPlayerData()
    {
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest
            {
                
            }, result =>
            {
                //�v���C���[�ɂ��ꂼ��̒l��n��
                player._name = result.Data["Name"].Value;
                var playerImage = ItemList.Instance.allItems.Find(item => item.name == result.Data["Image"].Value);
                if (playerImage == null)
                {
                    throw new System.IO.FileNotFoundException("������܂���ł���");
                }
                //����n��
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

    public void InputValueChanged()
    {
        //�����������̐ݒ�
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
                {"Image","�X�L��1" },
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
                shop.GetCatalogData("main");
                userUI.gameObject.SetActive(true);
                PlayerUpdateUserTitleDisplayName(inputName.text);
                PlayerUpdateUserData(inputName.text);
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    //�A�J�E���g�쐬���ɌĂяo��
    public void InputComplete()
    {
        PlayFabInitPlayer();
    }
    // Start is called before the first frame update
    void Start()
    {
        //���Ƀ��O�C�����Ă��邩�̊m�F
        if (GameManager.Instance.OnLogin)
        {
            LoginButton.SetActive(true);
            Menu.SetActive(false);
            GameManager.Instance.OnLogin = false;
        }
        else
        {
            LoginButton.SetActive(false);
            Menu.SetActive(true);
            Menu.SetActive(true);
            LoginButton.SetActive(false);
            Inventry.GetCatalogData("main");
            shop.GetCatalogData("main");
            userUI.gameObject.SetActive(true);
            GetPlayerData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}