using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList Instance;
    public List<Item> allItems;//�Q�[���ɂ���S�ẴA�C�e���f�[�^���X�g
    public List<Item> inventoryItems;//�C���x���g���ɂ���A�C�e���f�[�^���X�g
    public List<Item> shopItems;//�V���b�v�ɂ���A�C�e���f�[�^���X�g
    private void Awake()
    {
        Instance = this;
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
