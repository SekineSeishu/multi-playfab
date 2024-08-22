using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneController : MonoBehaviour
{
    [SerializeField] private LobbyUIManager lobbyUIManager;//���r�[UI�̊Ǘ�
    private GameObject playerPrefab;//�v���C���[�I�u�W�F�N�g
    private NetworkRunner runner;//�l�b�g���[�N�����i�[
    private string lobbyName;//���r�[��
    [SerializeField] private string homeSceneName = "";//���j���[�V�[��������
    [SerializeField] private GameObject backHomeUI;//���j���[��ʂɖ߂�
    [SerializeField] private LobbyState lobbyState;//���r�[���
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
    //�ޏo��ʂ��o��
    public void BackHomeMenu()
    {
        backHomeUI.SetActive(true);
    }

    //�ޏo��ʂ�����
    public void BackLobby()
    {
        backHomeUI.SetActive(false);
    }
    //���j���[�V�[���ɖ߂�
    public void BackHome()
    {
        SceneManager.LoadScene(homeSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
