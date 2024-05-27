using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabInventry : MonoBehaviour
{
    public static PlayFabInventry Instance;
    public List<ItemInstance> userInventry;
    public List<Item> AllItems;
    public List<CatalogItem> CatalogItems { get; private set; }
    private bool Inventorystop;
    public Inventry Inventory;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Inventorystop = true;
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
        var userInventoryRequest = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest
        {
        }, result =>
         {
             Debug.Log($"インベントリの情報の取得に成功 : インベントリに入っているアイテム数 {result.Inventory.Count}個");
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
                     Inventorystop = true;
                 }
                 GetItemDescription(item.ItemId,item.DisplayName,item.ItemInstanceId,itemCounts[itemId]);
            }
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
        var matchingItem = AllItems.Find(item => item.name == itemDisplayName);
        var guids = UnityEditor.AssetDatabase.FindAssets(itemName);
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

        if (Inventorystop)
        {
            Debug.Log("in");
            Inventory.Add(obj);
            Inventorystop = false;
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
