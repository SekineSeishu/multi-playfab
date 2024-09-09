using PlayFab.EconomyModels;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //�X���b�g�|�W�V�����̐e�I�u�W�F�N�g
    public Transform content;

    [SerializeField] private GameObject _itemSlot;

    //�X���b�g�̏��������ʒu���X�g
    [SerializeField] private List<Transform> _firstSlotsPosition;

    //�A�C�e���X���b�g�̈ʒu���X�g
    [SerializeField]private List<Transform> _slotsPosition;

    //�A�C�e���X���b�g�̈ʒu�I�u�W�F�N�g
    [SerializeField] private GameObject _slotPositionPrefab;

    private GameObject currentRow;
    private int itemPreRow = 4;
    private int currentItemIndex;

    //���̒i��Y�l
    private float _nextLevelPositionY = -400;

    // Start is called before the first frame update
    void Start()
    {
        ClearInventory();
        //���X�g�̏����擾
        _slotsPosition = new List<Transform>(_firstSlotsPosition);
    }

    //�A�C�e���̓��ꂽ�X���b�g�𐶐�����
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

    //�A�C�e���X���b�g�𐶐����邽�тɃX���b�g�ʒu�𑝂₷
    private void slotUp(Transform slotPosition)
    {
        //���̒i��_slotPositionPrefab�𐶐�����
        GameObject newslot = Instantiate(_slotPositionPrefab,content);
        newslot.transform.position = new Vector3(slotPosition.position.x,
                                                                      slotPosition.position.y + _nextLevelPositionY,
                                                                      slotPosition.position.z);
        _slotsPosition.Add(newslot.transform);
    }

    //�C���x���g�����Z�b�g
    private void ClearInventory()
    {
        currentItemIndex = 0;
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
    }

}
