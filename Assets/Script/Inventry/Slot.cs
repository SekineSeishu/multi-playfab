using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public Item item;

    //�A�C�e���ǉ�
    public void AddItem(Item newItem)
    {
        Debug.Log(newItem);
        Debug.Log(gameObject.name);
        item = newItem;
        icon.sprite = newItem.icon;
    }

    //�A�C�e����菜��
    public void ClearItem()
    {
        item = null;
        icon.sprite = null;
    }

    //�A�C�e������
    public void OnRemoveButton()
    {
        Inventry.instance.Remove(item);
    }

    //�A�C�e���g�p
    public void UseItem()
    {
        item.see();
    }
    //�V���b�v�A�C�e��������
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
