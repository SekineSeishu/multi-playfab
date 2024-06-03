using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    //�A�C�e����
    new public string name = "New Item";
    //�A�C�e������
    public string text;
    //�A�C�e���A�C�R��
    public Sprite icon = null;
    //�A�C�e��ID
    public string ItemID;
    //�A�C�e����
    public int itemCounts = 0;
    //�A�C�e���̒l�i(�V���b�v�l)
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
                setSkinText.text = "�ݒ蒆";
            }
            else
            {
                setSkinText.text = "�ݒ�";
            }
        }
        dataName.text = name.ToString();
        dataText.text = "GD�F" + ShopItemPrice.ToString();
        Icon.sprite = icon;
    }

    public void SetSkin()
    {
        if(setSkinText.text == "�ݒ�")
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
            setSkinText.text = "�ݒ蒆";
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
