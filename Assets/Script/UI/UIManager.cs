using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;

public class UIManager : MonoBehaviour
{
    private RoomManager roomManager;
    public Button createRoomButton;
    public Button joinRoomButton;
    [SerializeField] private TMP_InputField inputRoomName;
    private int maxPlayers = 4;
    public Transform roomListContainer;
    public GameObject roomListItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        roomManager = GetComponent<RoomManager>();
    }

    public void On()
    {
        Debug.Log("click");
        roomManager.CreateRoom(inputRoomName.text, maxPlayers);
    }

    public void offf()
    {
        roomManager.JoinRoom(inputRoomName.text);
    }

    public void UpdateRoomList(List<SessionInfo> sessionList)
    {
        Debug.LogError("i");
        foreach(Transform child in roomListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach(var session in sessionList)
        {
            Debug.LogError("u");
            GameObject roomItem = Instantiate(roomListItemPrefab,roomListContainer);
            roomItem.GetComponentInChildren<RoomData>().nameText.text = session.Name;
            roomItem.GetComponent<Button>().onClick.AddListener(() => roomManager.JoinRoom(session.Name));
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
