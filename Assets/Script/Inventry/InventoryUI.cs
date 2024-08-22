using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //�X���b�g�|�W�V�����̐e�I�u�W�F�N�g
    public Transform InventryPanel;

    [SerializeField] private GameObject _itemSlot;

    //�X���b�g�̏��������ʒu���X�g
    [SerializeField] private List<Transform> _firstSlotsPosition;

    //�A�C�e���X���b�g�̈ʒu���X�g
    [SerializeField]private List<Transform> _slotsPosition;

    //�A�C�e���X���b�g�̈ʒu�I�u�W�F�N�g
    [SerializeField] private GameObject _slotPositionPrefab;

    //���̒i��Y�l
    private float _nextLevelPositionY = -400;

    // Start is called before the first frame update
    void Start()
    {
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
            GameObject slot = Instantiate(_itemSlot, _slotsPosition[i]);
            slot.GetComponent<Slot>().AddItem(items[i]);
            slotUp(_slotsPosition[i]);
            Debug.Log(i);
            //slotr++;
        }
    }

    //�A�C�e���X���b�g�𐶐����邽�тɃX���b�g�ʒu�𑝂₷
    private void slotUp(Transform slotPosition)
    {
        //���̒i��_slotPositionPrefab�𐶐�����
        GameObject newslot = Instantiate(_slotPositionPrefab,InventryPanel);
        newslot.transform.position = new Vector3(slotPosition.position.x,
                                                                      slotPosition.position.y + _nextLevelPositionY,
                                                                      slotPosition.position.z);
        _slotsPosition.Add(newslot.transform);
    }

    //�C���x���g�����Z�b�g
    private void ClearInventory()
    {
        // _slotsPosition �̊e�ʒu�̎q�I�u�W�F�N�g���폜
        for (int i = 0;i < _slotsPosition.Count;i++)
        {
            //����_slotPositionPrefab�͎q�I�u�W�F�N�g�̂ݍ폜
            if (i <= _firstSlotsPosition.Count)
            {
                foreach(Transform child in _slotsPosition[i])
                {
                    Destroy(child.gameObject);
                }
            }
            //�ǉ�����_slotPositionPrefab�͂��ꂲ�ƍ폜
            else
            {
                Destroy(_slotsPosition[i].gameObject);
            }
        }
        //���X�g�̏�����
        _slotsPosition.Clear();
        _slotsPosition = new List<Transform>(_firstSlotsPosition);
    }

}
