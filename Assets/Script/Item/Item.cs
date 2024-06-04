
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
    //�L�����o�X
    public GameObject ItemdataUI;
    //�V���b�v�L�����o�X
    public GameObject ShopItemdataUI;
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    
    }

    //�A�C�e���w��
    public void buy()
    {
        PlayfabShop.Instance.PurchaseItem("main", "gold_store", ItemID, "GD", (int)ShopItemPrice);
    }

    //�A�C�e���g�p
    public void Use()
    {
        itemCounts--;
        PlayFabInventry.Instance.ConSumeItem(ItemID);
        if (itemCounts == 0)
        {
            Inventory.instance.Remove(this);
        }
        Debug.Log(name + "���g�p���܂���");
    }

    //�A�C�e���ڍׂ�����
    public void see()
    {
        GameObject data = Instantiate(ItemdataUI);
        ItemData itemData = data.GetComponent<ItemData>();
        itemData.name = name;
        itemData.text = text;
        itemData.icon = icon;
    }

    //�V���b�v�A�C�e���̏ڍׂ�����
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
