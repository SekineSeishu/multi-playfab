using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventryUI : MonoBehaviour
{
    public Transform InventryPanel;
    public Transform slotPanel;

    public Slot[] slots;

    [SerializeField] private GameObject Itemslot;

    [SerializeField] private List<Transform> firstSlotsPosition;

    [SerializeField]private List<Transform> slotsPosition;

    [SerializeField] private GameObject slotPositionPrefab;

    public int slotr = 0;

    // Start is called before the first frame update
    void Start()
    {
        slots = InventryPanel.GetComponentsInChildren<Slot>();
        firstSlotsPosition = slotsPosition;
    }

    // Update is called once per frame
    public void UpdateUI(List<Item> items)
    {
        ClearInventory();
        Debug.Log("UpdateUI");
        foreach (var item in items)
        {
            GameObject slot = Instantiate(Itemslot, slotsPosition[slotr]);
            //slot.transform.SetParent(slotPanel, false);
            slot.GetComponent<Slot>().AddItem(item);
            slotUp(slotsPosition[slotr]);
            slotr++;  
        }
    }

    private void slotUp(Transform slotPosition)
    {
        Vector3 newPosition = slotPosition.localPosition;
        newPosition.y -= 400;
        var newslot = Instantiate(slotPositionPrefab, newPosition, Quaternion.identity);
        newslot.transform.SetParent(InventryPanel, false);
        slotsPosition.Add(newslot.transform);
    }

    private void ClearInventory()
    {
        // slotsPosition の各位置の子オブジェクトを削除
        foreach (var slotPos in slotsPosition)
        {
            foreach (Transform child in slotPos)
            {
                Destroy(child.gameObject);
                slotr = 0;
                slotsPosition.Clear();
                slotsPosition.AddRange(firstSlotsPosition);
            }
        }
    }

}
