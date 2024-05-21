using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class NetworkSceneManager : MonoBehaviour
{
    public void OnServerInitialized(NetworkRunner runner)
    {
        runner.ProvideInput = true;
    }

    public void LoadScene(string sceneName,LoadSceneMode loadSceneMode)
    {
        SceneManager.LoadScene(sceneName, loadSceneMode);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void SetActiveScene(string sceneName)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
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
