using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PlayFab.ClientModels;
using PlayFab;
using System.Linq;

public class PlayfabShop : MonoBehaviour
{
    public static PlayfabShop Instance;
    public List<Item> GetShopItems = new List<Item>();
    public List<CatalogItem> CatalogItems { get; private set; }
    public List<StoreItem> StoreItems { get; private set; }

    public Inventory shopItemGrop;

    private int allItemCount = 0;
    private int nowItemCount = 0;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {
        
    }

    //サーバー内にあるアイテムの情報の取得
    public void GetCatalogData(string catalogVersion)
    {
        // アイテムのカタログ情報を取得するリクエストを作成
        // PlayFabクライアントでリクエストを実行
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest()
        {
            CatalogVersion = catalogVersion,
        }
        , result =>
        {
            Debug.Log("カタログデータ取得成功");
            CatalogItems = result.Catalog;

            GetStoreData("main", "gold_store");
        }
        , error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    //ストアデータの取得
    public void GetStoreData(string catalogVersion, string storeId)
    {
        PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest()
        {
            CatalogVersion = catalogVersion,
            StoreId = storeId
        }
        , result =>
        {
            Debug.Log("ストアデータ取得成功" + result.Store.Count);
            
            StoreItems = result.Store;

            
            foreach (var storeItem in StoreItems)
            {
                var catalogItem = CatalogItems.Find(x => x.ItemId == storeItem.ItemId);
                if (catalogItem != null)
                {
                    //ストア内のアイテム情報を取得
                    string itemId = storeItem.ItemId;
                    string displayName = catalogItem.DisplayName;
                    string description = catalogItem.Description;
                    uint price = storeItem.VirtualCurrencyPrices["GD"];

                    allItemCount = result.Store.Count;
                    ShopFind(itemId,displayName, (int)price);
                }
                else
                {
                    Debug.LogError("アイテム無し");
                }

                
            }
        }
        , error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void ShopFind(string itemId ,string itemName,int price)
    {
        //AllItemsの中から一致するものを探す
        var matchingItem = ItemList.Instance.allItems.Find(item => item.name == itemName);
        Debug.Log("t:" + itemName);
        if (matchingItem == null)
        {
            throw new System.IO.FileNotFoundException("見つかりませんでした");
        }
        var obj = matchingItem as Item;

        Debug.Log(obj.name);
        obj.ItemID = itemId;
        obj.name = itemName;
        obj.ShopItemPrice = price;

        //インベントリに同じアイテムがないか探す
        var shopItem = PlayFabInventry.Instance.userInventry.Find(item => item.name == itemName);
        if (shopItem == null)
        {
            //なかったら追加
            Debug.Log(obj.name);
            GetShopItems.Add(obj);
            nowItemCount++;
        }
        else
        {
            allItemCount--;
        }
        if (nowItemCount == allItemCount)
        {
            shopItemGrop.Add(GetShopItems);
        }
    }

    public void OpenShop()
    {
        var inventory = Instantiate(shopItemGrop.gameObject, shopItemGrop.transform);
        inventory.transform.parent = gameObject.transform;
        shopItemGrop.Add(GetShopItems);
    }

    //選択アイテムの購入
    public void PurchaseItem(string catalogVersion, string storeId, string itemId, string virtualCurrency, int price)
    {
        PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
        {
            CatalogVersion = catalogVersion,
            StoreId = storeId,
            ItemId = itemId,
            VirtualCurrency = virtualCurrency,
            Price = price
        }
        , purchaseResult =>
        {
            Debug.Log($"{purchaseResult.Items[0].DisplayName}購入成功！");
            PlayFabInventry.Instance.GetUserInventory();
            GetShopItems.Clear();
            GetCatalogData("main");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

}
