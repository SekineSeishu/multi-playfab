using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomData : MonoBehaviour
{
    [SerializeField]
    private TMP_Text idText;
    [SerializeField]
    private TMP_Text playerCountText;
    [SerializeField]
    private TMP_Text nameText;

    public void SetData(string id, string playerCount,string name)
    {
        idText.text = id;
        playerCountText.text = playerCount;
        nameText.text = name;
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
