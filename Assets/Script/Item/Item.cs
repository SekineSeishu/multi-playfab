
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

    //アイテム詳細を見る
    public void see()
    {
        Debug.Log(name + "のアイテム詳細");
        GameObject data = Instantiate(ItemdataUI);
        ItemData itemData = data.GetComponent<ItemData>();
        itemData.name = name;
        itemData.text = text;
        itemData.icon = icon;
    }

    //ショップアイテムの詳細を見る
    public void shopItemSee()
    {
        Debug.Log(name + "のアイテム詳細");
        GameObject data = Instantiate(ShopItemdataUI);
        ItemData itemData = data.GetComponent<ItemData>();
        itemData.ItemID = ItemID;
        itemData.name = name;
        itemData.text = text;
        itemData.icon = icon;
        itemData.ShopItemPrice = ShopItemPrice;
    }
}
