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
    public List<Item> AllItems;
    public List<CatalogItem> CatalogItems { get; private set; }
    public List<StoreItem> StoreItems { get; private set; }

    public ShopInventory shopItemGrop;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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

            GetStoreData("main", "gold_store");
        }
        , error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void GetStoreData(string catalogVersion, string storeId)
    {
        PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest()
        {
            CatalogVersion = catalogVersion,
            StoreId = storeId
        }
        , result =>
        {
            Debug.Log("ストアデータ取得成功");
            StoreItems = result.Store;

            foreach (var storeItem in StoreItems)
            {
                var catalogItem = CatalogItems.Find(x => x.ItemId == storeItem.ItemId);
                if (catalogItem != null)
                {
                    string itemId = storeItem.ItemId;
                    string displayName = catalogItem.DisplayName;
                    string description = catalogItem.Description;
                    uint price = storeItem.VirtualCurrencyPrices["GD"];

                    //ShopFind(itemId,displayName, (int)price);
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

    /*public void ShopFind(string itemId ,string itemName,int price)
    {
        var matchingItem = AllItems.Find(item => item.name == itemName);
        //var guids = UnityEditor.AssetDatabase.FindAssets(itemName);
        Debug.Log("t:" + itemName);
        if (matchingItem == null)
        {
            throw new System.IO.FileNotFoundException("見つかりませんでした");
        }
        //var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        var obj = matchingItem as Item;

        Debug.Log(obj.name);
        obj.ItemID = itemId;
        obj.ShopItemPrice = price;
        shopItemGrop.Add(obj);
    }*/

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
            ShopInventory.instance.AllClear();
            GetCatalogData("main");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void OnClick()
    {
        var item = StoreItems.FirstOrDefault();
        PurchaseItem("main", "gold_store", item.ItemId, "GD", (int)item.VirtualCurrencyPrices["GD"]);
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
