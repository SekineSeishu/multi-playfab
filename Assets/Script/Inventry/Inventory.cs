using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private InventoryUI inventryUI;//�C���x���g��UI
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    //�C���x���g���ɓ����Ă���A�C�e���ꗗ
    public List<Item> items = new List<Item>();

    //�C���x���g���ɃA�C�e���X���b�g�ǉ�
    public void Add(List<Item> item)
    {
        Debug.Log("Add");
        items = item; 
        inventryUI.UpdateUI(items);
    }
    
    //�A�C�e�����O��
    public void Remove(Item item)
    {
        items.Remove(item);
        inventryUI.UpdateUI(items);
    }

    //�C���x���g�����Z�b�g
    public void AllClear()
    {
        Debug.Log("�C���x���g�����Z�b�g");
        items.Clear();
        inventryUI.UpdateUI(items);
    }
}
