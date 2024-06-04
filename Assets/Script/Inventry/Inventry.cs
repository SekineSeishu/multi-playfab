using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventry : MonoBehaviour
{
    public static Inventry instance;
    InventryUI inventryUI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        inventryUI = GetComponent<InventryUI>();
        //inventryUI.UpdateUI();
    }

    public List<Item> items = new List<Item>();

    public void Add(List<Item> item)
    {
        Debug.Log("Add");
        items = item; 
        inventryUI.UpdateUI(items);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        inventryUI.UpdateUI(items);
    }

    public void AllClear()
    {
        Debug.Log("aiueo");
        items.Clear();
        inventryUI.UpdateUI(items);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
