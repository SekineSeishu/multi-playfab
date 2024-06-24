using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform InventryPanel;

    [SerializeField] private GameObject Itemslot;

    //スロットの初期生成位置リスト
    [SerializeField] private List<Transform> firstSlotsPosition;

    //アイテムスロットの位置リスト
    [SerializeField]private List<Transform> slotsPosition;

    //アイテムスロットの位置オブジェクト
    [SerializeField] private GameObject slotPositionPrefab;

    public int slotr = 0;

    // Start is called before the first frame update
    void Start()
    {
        //リストの初期取得
        firstSlotsPosition = new List<Transform>(slotsPosition);
    }

    //アイテムの入れたスロットを生成する
    public void UpdateUI(List<Item> items)
    {
        ClearInventory();
        Debug.Log("UpdateUI");
        foreach (var item in items)
        {
            GameObject slot = Instantiate(Itemslot, slotsPosition[slotr]);
            slot.GetComponent<Slot>().AddItem(item);
            slotUp(slotsPosition[slotr]);
            slotr++;  
        }
    }

    //アイテムスロットを生成するたびにスロット位置を増やす
    private void slotUp(Transform slotPosition)
    {
        Vector3 newPosition = slotPosition.localPosition;
        newPosition.y -= 400;
        var newslot = Instantiate(slotPositionPrefab, newPosition, Quaternion.identity);
        newslot.transform.SetParent(InventryPanel, false);
        slotsPosition.Add(newslot.transform);
    }

    //インベントリリセット
    private void ClearInventory()
    {
        // slotsPosition の各位置の子オブジェクトを削除
        foreach (var slotPos in slotsPosition)
        {
            foreach (Transform child in slotPos)
            {
                Destroy(child.gameObject);
            }
        }
        slotr = 0;
        slotsPosition.Clear();
        slotsPosition = new List<Transform>(firstSlotsPosition);
    }

}
