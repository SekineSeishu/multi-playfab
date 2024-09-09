using PlayFab.EconomyModels;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //スロットポジションの親オブジェクト
    public Transform content;

    [SerializeField] private GameObject _itemSlot;

    //スロットの初期生成位置リスト
    [SerializeField] private List<Transform> _firstSlotsPosition;

    //アイテムスロットの位置リスト
    [SerializeField]private List<Transform> _slotsPosition;

    //アイテムスロットの位置オブジェクト
    [SerializeField] private GameObject _slotPositionPrefab;

    private GameObject currentRow;
    private int itemPreRow = 4;
    private int currentItemIndex;

    //次の段のY値
    private float _nextLevelPositionY = -400;

    // Start is called before the first frame update
    void Start()
    {
        ClearInventory();
        //リストの初期取得
        _slotsPosition = new List<Transform>(_firstSlotsPosition);
    }

    //アイテムの入れたスロットを生成する
    public void UpdateUI(List<Item> items)
    {
        ClearInventory();
        Debug.Log("UpdateUI");
        for (int i = 0; i < items.Count; i++)
        {
            if (currentItemIndex % itemPreRow == 0)
            {
                currentRow = Instantiate(_slotPositionPrefab, content);
            }
            GameObject slot = Instantiate(_itemSlot, currentRow.transform);
            slot.GetComponent<Slot>().AddItem(items[i]);
            //slotUp(_slotsPosition[i]);
            Debug.Log(i);
            currentItemIndex++;
        }
    }

    //アイテムスロットを生成するたびにスロット位置を増やす
    private void slotUp(Transform slotPosition)
    {
        //次の段に_slotPositionPrefabを生成する
        GameObject newslot = Instantiate(_slotPositionPrefab,content);
        newslot.transform.position = new Vector3(slotPosition.position.x,
                                                                      slotPosition.position.y + _nextLevelPositionY,
                                                                      slotPosition.position.z);
        _slotsPosition.Add(newslot.transform);
    }

    //インベントリリセット
    private void ClearInventory()
    {
        currentItemIndex = 0;
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
    }

}
