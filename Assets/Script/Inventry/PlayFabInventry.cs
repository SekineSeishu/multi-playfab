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
    public List<Item> userInventry = new List<Item>();//プレイヤーのインベントリリスト
    public List<CatalogItem> CatalogItems { get; private set; }//サーバーにあるアイテム情報
    private int Inventorycount = 0;//インベントリにあるアイテムの個数
    private int nowInventoryCount = 0;//取得し終わったアイテムの数
    public Inventory Inventory;//インベントリ
    [SerializeField] private TMP_Text coinText;//ゲーム内通貨の表示

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

    //サーバー内にあるアイテムの情報の取得
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
        //インベントリUIの中身を初期化
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
                 //同じアイテムが複数個あるかどうか
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
             //ゲーム内通貨の取得
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
        var matchingItem = ItemList.Instance.allItems.Find(item => item.name == itemDisplayName);
        Debug.Log("t:" + itemName);
        if (matchingItem == null)
        {
            throw new System.IO.FileNotFoundException("見つかりませんでした");
        }
        //アイテムの情報を渡す
        var obj = matchingItem;
        Debug.Log(obj.name);
        obj.ItemID = itemID;
        obj.name = itemDisplayName;
        obj.itemCounts = itemCount;
        obj.text = Descriotion;

        userInventry.Add(obj);
        nowInventoryCount++;
        Debug.Log("現在の数:" + nowInventoryCount);

        //インベントリの中身を全ての取得したら実行
        if (nowInventoryCount == Inventorycount)
        {
            Debug.Log("全て取得完了！");
            Inventory.Add(userInventry);
        }
    }

    public void OpenInventory()
    {
        var inventory = Instantiate(Inventory.gameObject,Inventory.transform);
        inventory.transform.parent = gameObject.transform;
        Inventory.Add(userInventry);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
