using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine.UI;
using UnityEditor.PackageManager;

public class PlayfabLogin : MonoBehaviour
{
    public PlayfabShop shop;
    public PlayFabInventry Inventry;
    [SerializeField] private GameObject nametext;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject LoginButton;
    [SerializeField] private Player player;
    [SerializeField] private UserProfielUI userUI;
    [SerializeField] private string userRank;
    [SerializeField] private string userExp;

    public void Login()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }

    private void PlayFabAuthService_OnLoginSuccess(LoginResult success)
    {
        Debug.Log("ログイン成功");
        //新規作成したかどうか
        if (success.NewlyCreated)
        {
            //ユーザー名入力
            nametext.SetActive(true);
            InputValueChanged();
        }
        else
        {
            //メインメニュー転移
            Menu.SetActive(true);
            LoginButton.SetActive(false);
            Inventry.GetCatalogData("main");
            shop.GetCatalogData("main");
            userUI.gameObject.SetActive(true);
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
                    throw new System.IO.FileNotFoundException("見つかりませんでした");
                }
                player.icon = playerImage.icon;
                userRank = result.Data["Rank"].Value;
                userExp = result.Data["Exp"].Value;
                userUI.gameObject.SetActive(true);
                userUI.SetProfiel(player._name,player.icon);
            }, error =>
            {
                Debug.LogError(error.GenerateErrorReport());
            });
    }


    //表示名の入力コントロール
    [SerializeField] TMP_InputField inputName;
    //完了ボタン
    [SerializeField] Button inputComp;

    public void PlayerUpdateUserTitleDisplayName(string name)
    {
        if (string.IsNullOrEmpty(inputName.text))
        {
            Debug.LogError("入力された名前が空です。");
            return;
        }
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        }, result =>
        {
            Debug.Log("プレイヤー名:" + result.DisplayName);
        }, error => Debug.LogError("プレイヤー名の設定エラー: " + error.GenerateErrorReport()));
    }

    public void PlayerNameUpdate()
    {
        nametext.SetActive(true);
        InputValueChanged();
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
        //表示名は3文字以上１０文字以内
        return !string.IsNullOrWhiteSpace(inputName.text)
            && 3 <= inputName.text.Length
            && inputName.text.Length <= 10;
    }
    #region プレイヤー表示の更新
    private void UpdateUserTitleDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputName.text
        }, result =>
        {
            Debug.Log("プレイヤー名:" + result.DisplayName);
            /*nametext.SetActive(false);
            Inventry.GetCatalogData("main");
            Menu.SetActive(true);
            LoginButton.SetActive(false);
            shop.GetCatalogData("main");*/
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion

    #region プレイヤーの初期化
    public void InitPlayer()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                {"Name", inputName.text},
                {"Image","魔法使い" },
                {"Exp",userExp},
                {"Rank",userRank}
            }
        };

        PlayFabClientAPI.UpdateUserData(request
            , result =>
            {
                Debug.Log("プレイヤーの初期化完了");
                //UpdateUserTitleDisplayName();
                nametext.SetActive(false);
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
                {"Exp","０" },
                {"Rank","１" },
                {"Name","" },
                {"Image","魔法使い" },
            }
        };

        PlayFabClientAPI.UpdateUserData(request
            , result =>
            {
                Debug.Log("プレイヤーの初期化完了");
                nametext.SetActive(false);
                Menu.SetActive(true);
                LoginButton.SetActive(false);
                Inventry.GetCatalogData("main");
                shop.GetCatalogData("main");
                userUI.gameObject.SetActive(true);
                //GetPlayerData(/*success.PlayFabId*/);

                PlayerUpdateUserData(inputName.text);
                PlayerUpdateUserTitleDisplayName(inputName.text);
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    public void InputComplete()
    {
        PlayFabInitPlayer();
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
        Debug.Log("ログイン失敗");
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