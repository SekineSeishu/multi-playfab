using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneController : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUIManager;//ロビーUIの管理
    private GameObject playerPrefab;//プレイヤーオブジェクト
    private NetworkRunner runner;//ネットワークランナー
    private string lobbyName;//ロビー名
    [SerializeField] private string homeSceneName = "";//メニューシーン名入力
    [SerializeField] private GameObject backHomeUI;//メニュー画面に戻る
    [SerializeField] private LobbyState lobbyState;//ロビー情報
    public bool spaw;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            spaw = true;
            lobbyName = GameManager.Instance.CurrentLobbyName;
            runner = LobbyManager.Instance._runner;
            OnPlayerJoined(runner);
        }
    }
    public void OnPlayerJoined(NetworkRunner runner)
    {
    }
    //退出画面を出す
    public void BackHomeMenu()
    {
        backHomeUI.SetActive(true);
    }

    //退出画面を消す
    public void BackLobby()
    {
        backHomeUI.SetActive(false);
    }
    //メニューシーンに戻る
    public void BackHome()
    {
        SceneManager.LoadScene(homeSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
