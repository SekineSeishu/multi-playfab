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

    //�T�[�o�[���ɂ���A�C�e���̏��̎擾
    public void GetCatalogData(string catalogVersion)
    {
        // �A�C�e���̃J�^���O�����擾���郊�N�G�X�g���쐬
        // PlayFab�N���C�A���g�Ń��N�G�X�g�����s
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest()
        {
            CatalogVersion = catalogVersion,
        }
        , result =>
        {
            Debug.Log("�J�^���O�f�[�^�擾����");
            CatalogItems = result.Catalog;

            GetStoreData("main", "gold_store");
        }
        , error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    //�X�g�A�f�[�^�̎擾
    public void GetStoreData(string catalogVersion, string storeId)
    {
        PlayFabClientAPI.GetStoreItems(new GetStoreItemsRequest()
        {
            CatalogVersion = catalogVersion,
            StoreId = storeId
        }
        , result =>
        {
            Debug.Log("�X�g�A�f�[�^�擾����" + result.Store.Count);
            
            StoreItems = result.Store;

            
            foreach (var storeItem in StoreItems)
            {
                var catalogItem = CatalogItems.Find(x => x.ItemId == storeItem.ItemId);
                if (catalogItem != null)
                {
                    //�X�g�A���̃A�C�e�������擾
                    string itemId = storeItem.ItemId;
                    string displayName = catalogItem.DisplayName;
                    string description = catalogItem.Description;
                    uint price = storeItem.VirtualCurrencyPrices["GD"];

                    allItemCount = result.Store.Count;
                    ShopFind(itemId,displayName, (int)price);
                }
                else
                {
                    Debug.LogError("�A�C�e������");
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
        //AllItems�̒������v������̂�T��
        var matchingItem = ItemList.Instance.allItems.Find(item => item.name == itemName);
        Debug.Log("t:" + itemName);
        if (matchingItem == null)
        {
            throw new System.IO.FileNotFoundException("������܂���ł���");
        }
        var obj = matchingItem as Item;

        Debug.Log(obj.name);
        obj.ItemID = itemId;
        obj.name = itemName;
        obj.ShopItemPrice = price;

        //�C���x���g���ɓ����A�C�e�����Ȃ����T��
        var shopItem = PlayFabInventry.Instance.userInventry.Find(item => item.name == itemName);
        if (shopItem == null)
        {
            //�Ȃ�������ǉ�
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

    //�I���A�C�e���̍w��
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
            Debug.Log($"{purchaseResult.Items[0].DisplayName}�w�������I");
            PlayFabInventry.Instance.GetUserInventory();
            GetShopItems.Clear();
            GetCatalogData("main");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

}
