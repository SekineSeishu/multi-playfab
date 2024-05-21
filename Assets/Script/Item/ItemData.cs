using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    //�A�C�e����
    new public string name = "New Item";
    //�A�C�e������
    public string text;
    //�A�C�e���A�C�R��
    public Sprite icon = null;
    //�A�C�e��ID
    public string ItemID;
    //�A�C�e����
    public int itemCounts = 0;
    //�A�C�e���̒l�i(�V���b�v�l)
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
