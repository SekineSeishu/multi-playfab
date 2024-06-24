using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform InventryPanel;

    [SerializeField] private GameObject Itemslot;

    //�X���b�g�̏��������ʒu���X�g
    [SerializeField] private List<Transform> firstSlotsPosition;

    //�A�C�e���X���b�g�̈ʒu���X�g
    [SerializeField]private List<Transform> slotsPosition;

    //�A�C�e���X���b�g�̈ʒu�I�u�W�F�N�g
    [SerializeField] private GameObject slotPositionPrefab;

    public int slotr = 0;

    // Start is called before the first frame update
    void Start()
    {
        //���X�g�̏����擾
        firstSlotsPosition = new List<Transform>(slotsPosition);
    }

    //�A�C�e���̓��ꂽ�X���b�g�𐶐�����
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

    //�A�C�e���X���b�g�𐶐����邽�тɃX���b�g�ʒu�𑝂₷
    private void slotUp(Transform slotPosition)
    {
        Vector3 newPosition = slotPosition.localPosition;
        newPosition.y -= 400;
        var newslot = Instantiate(slotPositionPrefab, newPosition, Quaternion.identity);
        newslot.transform.SetParent(InventryPanel, false);
        slotsPosition.Add(newslot.transform);
    }

    //�C���x���g�����Z�b�g
    private void ClearInventory()
    {
        // slotsPosition �̊e�ʒu�̎q�I�u�W�F�N�g���폜
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
