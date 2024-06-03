using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public Item item;

    //アイテム追加
    public void AddItem(Item newItem)
    {
        Debug.Log(newItem);
        Debug.Log(gameObject.name);
        item = newItem;
        icon.sprite = newItem.icon;
    }

    //アイテム取り除く
    public void ClearItem()
    {
        item = null;
        icon.sprite = null;
    }

    //アイテム消去
    public void OnRemoveButton()
    {
        Inventry.instance.Remove(item);
    }

    //アイテム使用
    public void UseItem()
    {
        item.see();
    }
    //ショップアイテムを見る
    public void ShopUseItem()
    {
        item.shopItemSee();
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
