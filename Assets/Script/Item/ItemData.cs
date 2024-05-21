using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    //アイテム名
    new public string name = "New Item";
    //アイテム説明
    public string text;
    //アイテムアイコン
    public Sprite icon = null;
    //アイテムID
    public string ItemID;
    //アイテム数
    public int itemCounts = 0;
    //アイテムの値段(ショップ値)
    public int ShopItemPrice;

    public TextMeshProUGUI dataName;
    public TextMeshProUGUI dataText;
    public Image Icon;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        dataName.text = name.ToString();
        dataText.text = text.ToString();
        Icon.sprite = icon;
    }

    public void Close()
    {
        Destroy(UI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
