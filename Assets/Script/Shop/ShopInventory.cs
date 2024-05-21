using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public static ShopInventory instance;
    public ShopInventoryUI inventryUI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        inventryUI.UpdateUI();
    }

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        items.Add(item);
        inventryUI.UpdateUI();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        inventryUI.UpdateUI();
    }

    public void AllClear()
    {
        items.Clear();
    }
}
