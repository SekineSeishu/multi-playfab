using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    //アイテム名
    new public string name = "New Item";
    //アイテム説明
    public string text;
    //アイテムアイコン
    public Sprite icon = null;
    //アイテムID
    public string ItemID;
    //アイテム数
    public int itemCounts = 0;
    //アイテムの値段(ショップ値)
    public int ShopItemPrice;

    [SerializeField] private Player player;
    public TextMeshProUGUI dataName;
    public TextMeshProUGUI dataText;
    public TextMeshProUGUI setSkinText;
    public Image Icon;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        if (setSkinText != null)
        {
            if (icon.name == player.icon.name)
            {
                setSkinText.text = "設定中";
            }
            else
            {
                setSkinText.text = "設定";
            }
        }
        dataName.text = name.ToString();
        dataText.text = "GD：" + ShopItemPrice.ToString();
        Icon.sprite = icon;
    }

    public void SetSkin()
    {
        if(setSkinText.text == "設定")
        {
            var request = new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>
            {
                { "Image", name },
            }
            };

            PlayFabClientAPI.UpdateUserData(request, OnSuccess, OnError);

            void OnSuccess(UpdateUserDataResult result)
            {
                Debug.Log("Success");
                PlayfabLogin.Instance.GetPlayerData();
            }
            void OnError(PlayFabError error)
            {
                Debug.LogError(error.GenerateErrorReport());
            }
            setSkinText.text = "設定中";
        }
        
    }

    public void buy()
    {
        PlayfabShop.Instance.PurchaseItem("main", "gold_store", ItemID, "GD", (int)ShopItemPrice);
        Close();
    }

    public void Close()
    {
        Destroy(UI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
