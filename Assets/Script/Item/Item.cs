
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
        Debug.Log(name + "���g�p���܂���");
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
