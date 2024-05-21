
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

    public void buy()
    {
        PlayfabShop.Instance.PurchaseItem("main", "gold_store", ItemID, "GD", (int)ShopItemPrice);
    }

    public void Use()
    {
        itemCounts--;
        PlayFabInventry.Instance.ConSumeItem(ItemID);
        if (itemCounts == 0)
        {
            Inventry.instance.Remove(this);
        }
        Debug.Log(name + "を使用しました");
    }

    public void see()
    {
        GameObject data = Instantiate(ItemdataUI);
        //data.transform.parent = ItemdataUI.transform;
        ItemData itemData = data.GetComponent<ItemData>();
        itemData.name = name;
        itemData.text = text;
        itemData.icon = icon;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
