using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventryUI : MonoBehaviour
{
    public Transform InventryPanel;
    public Transform slotPanel;

    public Slot[] slots;

    public GameObject Itemslot;

    public List<Transform> slotsPosition;

    public GameObject slotPositionPrefab;

    public int slotr = 0;

    // Start is called before the first frame update
    void Start()
    {
        slots = InventryPanel.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    public void UpdateUI(List<Item> items)
    {
        //Debug.Log("UpdateUI");
        /*for (int i = 0; i < slots.Length; i++)
        {
            if (i < Inventry.instance.items.Count)
            {
                slots[i].AddItem(Inventry.instance.items[i]);
            }
            else
            {
                slots[i].ClearItem();
            }
        }*/

        /*foreach (Transform child in slotPanel)
        {
            Destroy(child.gameObject);
        }*/

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

}
