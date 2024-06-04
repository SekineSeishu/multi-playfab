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
        Debug.Log("ログイン成功");
        //新規作成したかどうか
        if (success.NewlyCreated)
        {
            LogionButton.SetActive(false);
            //ユーザー名入力
            nameInput.SetActive(true);
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
            GetPlayerData();
        }
    }

    //ユーザーの情報を取得してPlayerに渡す
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
                    throw new System.IO.FileNotFoundException("見つかりませんでした");
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


    //表示名の入力コントロール
    [SerializeField] TMP_InputField inputName;
    //完了ボタン
    [SerializeField] Button inputComp;

    //名前が入力されているかの確認
    public void RegisterAccount()
    {
        string name = inputName.text;
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("Nameが空です。");
            return;
        }
        InputComplete();
    }
    //PlayFabのユーザーアカウントの表示名に名前を渡す
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

    //名前の変更時呼び出す

    //名前記入時の更新
    public void InputValueChanged()
    {
        inputComp.interactable = IsValidName();
    }

    //名前の文字数制限
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
    //名前の変更時呼び出す
    public void PlayerNameUpdate()
    {
        updateNameInput.SetActive(true);
        InputValueChanged();
    }

    //表示名の入力コントロール
    [SerializeField] TMP_InputField upDateInputName;
    //完了ボタン
    [SerializeField] Button updateInputComp;
    //名前の変更
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

    //初期化の際に呼び出す
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