using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    //Itemデータ
    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        //設定したアイコン
        GetComponent<Image>().sprite = item.icon;
    }

    //インベントリにアイテム追加
    public void PickUp()
    {
        Debug.LogError("im");
        //Inventry.instance.Add(item);
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
