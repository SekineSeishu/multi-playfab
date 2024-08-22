using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //スロットポジションの親オブジェクト
    public Transform InventryPanel;

    [SerializeField] private GameObject _itemSlot;

    //スロットの初期生成位置リスト
    [SerializeField] private List<Transform> _firstSlotsPosition;

    //アイテムスロットの位置リスト
    [SerializeField]private List<Transform> _slotsPosition;

    //アイテムスロットの位置オブジェクト
    [SerializeField] private GameObject _slotPositionPrefab;

    //次の段のY値
    private float _nextLevelPositionY = -400;

    // Start is called before the first frame update
    void Start()
    {
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
            GameObject slot = Instantiate(_itemSlot, _slotsPosition[i]);
            slot.GetComponent<Slot>().AddItem(items[i]);
            slotUp(_slotsPosition[i]);
            Debug.Log(i);
            //slotr++;
        }
    }

    //アイテムスロットを生成するたびにスロット位置を増やす
    private void slotUp(Transform slotPosition)
    {
        //次の段に_slotPositionPrefabを生成する
        GameObject newslot = Instantiate(_slotPositionPrefab,InventryPanel);
        newslot.transform.position = new Vector3(slotPosition.position.x,
                                                                      slotPosition.position.y + _nextLevelPositionY,
                                                                      slotPosition.position.z);
        _slotsPosition.Add(newslot.transform);
    }

    //インベントリリセット
    private void ClearInventory()
    {
        // _slotsPosition の各位置の子オブジェクトを削除
        for (int i = 0;i < _slotsPosition.Count;i++)
        {
            //初期_slotPositionPrefabは子オブジェクトのみ削除
            if (i <= _firstSlotsPosition.Count)
            {
                foreach(Transform child in _slotsPosition[i])
                {
                    Destroy(child.gameObject);
                }
            }
            //追加した_slotPositionPrefabはそれごと削除
            else
            {
                Destroy(_slotsPosition[i].gameObject);
            }
        }
        //リストの初期化
        _slotsPosition.Clear();
        _slotsPosition = new List<Transform>(_firstSlotsPosition);
    }

}
