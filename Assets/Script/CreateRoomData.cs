using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateRoomData : MonoBehaviour
{
    public Button createRoomButton;
    public TMP_InputField roomNameInputField;
    private int maxPlayers;
    [SerializeField]
    private Button upButtonl;
    [SerializeField] 
    private Button downButtonl;
    [SerializeField]
    private TMP_Text maxPlayerText;

    public void CreateOnClick()
    {
        RoomDataManager rm = GetComponentInParent<RoomDataManager>();
        rm.CreateRoom(roomNameInputField.text,maxPlayers);
    }
    public void UpPlayerOnClick()
    {
        maxPlayers++;
    }
    public void DownPlayerOnClick() 
    {
        maxPlayers--;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxPlayerText.text = maxPlayers.ToString();
    }
}
