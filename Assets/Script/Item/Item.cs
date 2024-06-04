
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PlayFab.ClientModels;
using PlayFab;
using System.Linq;

[CreateAssetMenu(fileName = "New Item",menuName = "ScriptableObject/Create Item")]
public class Item : ScriptableObject
{
    public static Item Instance;
    //キャンバス
    public GameObject ItemdataUI;
    //ショップキャンバス
    public GameObject ShopItemdataUI;
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    
    }

    //アイテム購入
    public void buy()
    {
        PlayfabShop.Instance.PurchaseItem("main", "gold_store", ItemID, "GD", (int)ShopItemPrice);
    }

    //アイテム使用
    public void Use()
    {
        itemCounts--;
        PlayFabInventry.Instance.ConSumeItem(ItemID);
        if (itemCounts == 0)
        {
            Inventory.instance.Remove(this);
        }
        Debug.Log(name + "を使用しました");
    }

    //アイテム詳細を見る
    public void see()
    {
        GameObject data = Instantiate(ItemdataUI);
        ItemData itemData = data.GetComponent<ItemData>();
        itemData.name = name;
        itemData.text = text;
        itemData.icon = icon;
    }

    //ショップアイテムの詳細を見る
    public void shopItemSee()
    {
        GameObject data = Instantiate(ShopItemdataUI);
        ItemData itemData = data.GetComponent<ItemData>();
        itemData.ItemID = ItemID;
        itemData.name = name;
        itemData.text = text;
        itemData.icon = icon;
        itemData.ShopItemPrice = ShopItemPrice;
    }
}
