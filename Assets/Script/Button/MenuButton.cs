using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject nowPase;//今表示しているページ
    public GameObject Inventory;//インベントリページ
    public GameObject Shop;//ショップページ
    public GameObject Gacha;//ガチャページ
    public GameObject machingMenu;//マッチングメニューページ

    //インベントリ表示
    public void OpenInventory()
    {
        Inventory.transform.localScale = new Vector3(1, 1, 1);
        nowPase = Inventory;
    }

    //ショップ表示
    public void OpenShop()
    {
        Shop.transform.localScale = new Vector3(1, 1, 1);
        nowPase = Shop;
    }

    //ガチャ表示
    public void OpenGacha()
    {
        Gacha.transform.localScale = new Vector3(1, 1, 1);
        nowPase = Gacha;
    }

    //マッチングメニュー表示
    public void OpenMachiMenu()
    {
        machingMenu.transform.localScale = new Vector3(1, 1, 1);
        nowPase = machingMenu;
    }
    //今開いているページを閉じる
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
