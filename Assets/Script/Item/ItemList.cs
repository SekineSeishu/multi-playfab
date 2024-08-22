using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList Instance;
    public List<Item> allItems;//ゲームにある全てのアイテムデータリスト
    public List<Item> inventoryItems;//インベントリにあるアイテムデータリスト
    public List<Item> shopItems;//ショップにあるアイテムデータリスト
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
