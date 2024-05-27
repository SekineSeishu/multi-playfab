using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject nowPase;
    public GameObject Inventory;
    public GameObject Shop;
    public GameObject Gacha;
    public GameObject machingMenu;
    public void OpenInventory()
    {
        Inventory.transform.localScale = new Vector3(1, 1, 1);
        nowPase = Inventory;
    }
    public void OpenShop()
    {
        Shop.transform.localScale = new Vector3(1, 1, 1);
        nowPase = Shop;
    }
    public void OpenGacha()
    {
        Gacha.transform.localScale = new Vector3(1, 1, 1);
        nowPase = Gacha;
    }
    public void OpenMachiMenu()
    {
        machingMenu.transform.localScale = new Vector3(1, 1, 1);
        nowPase = machingMenu;
    }
    public void Close()
    {
        nowPase.transform.localScale = new Vector3(0, 0, 0);
        nowPase = null;
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
