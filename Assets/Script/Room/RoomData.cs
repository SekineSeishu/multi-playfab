using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomData : MonoBehaviour
{
    public TMP_Text playerCountText;
    public TMP_Text nameText;
    public void SetData(string id, string playerCount,string name)
    {
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
