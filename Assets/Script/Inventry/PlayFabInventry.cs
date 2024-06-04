using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabInventry : MonoBehaviour
{
    public static PlayFabInventry Instance;
    public List<Item> userInventry = new List<Item>();
    public List<Item> AllItems;
    public List<CatalogItem> CatalogItems { get; private set; }
    private int Inventorycount = 0;
    private int nowInventoryCount = 0;
    private string nowItem;
    public Inventory Inventory;
    [SerializeField] private TMP_Text coinText;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }

    public void GetCatalogData(string catalogVersion)
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest()
        {
            CatalogVersion = catalogVersion,
        }
        , result =>
        {
            Debug.Log("カタログデータ取得成功");
            CatalogItems = result.Catalog;

            GetUserInventory();
        }
        , error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    //インベントリの情報を取得
    public void GetUserInventory()
    {
        Inventory.AllClear();
        userInventry.Clear();
        nowInventoryCount = 0;
        var userInventoryRequest = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest
        {
        }, result =>
         {
             Debug.Log($"インベントリの情報の取得に成功 : インベントリに入っているアイテム数 {result.Inventory.Count}個");
             Inventorycount = result.Inventory.Count;
             Dictionary<string, int> itemCounts = new Dictionary<string, int>();
             foreach (ItemInstance item in result.Inventory)
             {
                 string itemId = item.DisplayName;
                 Debug.Log($"ID : {item.ItemId}, Name : {item.DisplayName}, ItemInstanceId : {item.ItemInstanceId}");
                 if (itemCounts.ContainsKey(itemId))
                 {
                     itemCounts[itemId]++;

                 }
                 else
                 {
                     itemCounts[itemId] = 1;
                 }
                 GetItemDescription(item.ItemId,item.DisplayName,item.ItemInstanceId,itemCounts[itemId]);
            }
             string currencyInfo = "\n";
             foreach (var virtualCurrency in result.VirtualCurrency)
             {
                 Debug.Log($"仮想通貨{virtualCurrency.Key} : {virtualCurrency.Value}");
                 currencyInfo = $"{virtualCurrency.Key}: {virtualCurrency.Value}\n";
             }
                 coinText.text = currencyInfo;

         }, error =>
         {
             Debug.LogError($"インベントリの情報の取得に失敗");
         });
    }

    private void GetItemDescription(string itemId,string ItemName, string itemInstanceId,int itemCounts)
    {
            // アイテムのカタログ情報を取得するリクエストを作成
            CatalogItem targetItem = CatalogItems.Find(item => item.ItemId == itemId);
            // PlayFabクライアントでリクエストを実行

            if (targetItem != null)
            {
                // アイテムの説明文を取得して表示
                string description = targetItem.Description;
                Debug.Log("Item Description: " + description);

                // アイテムの情報を渡す
                Find(itemId, ItemName, itemInstanceId, itemCounts, description);
            }
            else
            {
                Debug.LogError("Item with ID " + itemId + " not found in catalog.");
            }
        }

    public  void Find(string itemName,string itemDisplayName,string itemID,int itemCount,string Descriotion)
    {
        //AllItemsの中から一致するものを探す
        var matchingItem = AllItems.Find(item => item.name == itemDisplayName);
        Debug.Log("t:" + itemName);
        if (matchingItem == null)
        {
            throw new System.IO.FileNotFoundException("見つかりませんでした");
        }
        var obj = matchingItem;
        Debug.Log(obj.name);
        obj.ItemID = itemID;
        obj.name = itemDisplayName;
        obj.itemCounts = itemCount;
        obj.text = Descriotion;

        userInventry.Add(obj);
        nowInventoryCount++;
        Debug.Log("現在の数:" + nowInventoryCount);

        if (nowInventoryCount == Inventorycount)
        {
            Debug.Log("a");
            Inventory.Add(userInventry);
        }
    }

    public void ConSumeItem(string ID)
    {
        Debug.Log(ID);

        var consumeItemRequest = new ConsumeItemRequest
        {
            ItemInstanceId = ID,
            ConsumeCount = 1,
        };
        Debug.Log("アイテム消費");
        PlayFabClientAPI.ConsumeItem(consumeItemRequest, OnSuccess, OnError);;
    }

    private void OnSuccess(ConsumeItemResult result)
    {
        Debug.Log($"インベントリのアイテム({result.ItemInstanceId}の消費に成功");
        GetUserInventory();
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError($"インベントリのアイテムの消費に失敗");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
