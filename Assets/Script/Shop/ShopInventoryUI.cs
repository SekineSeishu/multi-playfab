using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventoryUI : MonoBehaviour
{
    public Transform InventryPanel;

    public Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = InventryPanel.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        Debug.Log("UpdateUI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < ShopInventory.instance.items.Count)
            {
                slots[i].AddItem(ShopInventory.instance.items[i]);
            }
            else
            {
                slots[i].ClearItem();
            }
        }
    }
}
