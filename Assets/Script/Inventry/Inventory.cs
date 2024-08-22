using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private InventoryUI inventryUI;//インベントリUI
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    //インベントリに入っているアイテム一覧
    public List<Item> items = new List<Item>();

    //インベントリにアイテムスロット追加
    public void Add(List<Item> item)
    {
        Debug.Log("Add");
        items = item; 
        inventryUI.UpdateUI(items);
    }
    
    //アイテムを外す
    public void Remove(Item item)
    {
        items.Remove(item);
        inventryUI.UpdateUI(items);
    }

    //インベントリリセット
    public void AllClear()
    {
        Debug.Log("インベントリリセット");
        items.Clear();
        inventryUI.UpdateUI(items);
    }
}
