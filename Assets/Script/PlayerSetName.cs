using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSetName : MonoBehaviour
{
    public GameObject inputCanvas;
    public GameObject NetworkStart;
    public GameObject PlayerPrefab;
    private TMP_InputField inputField;
    private TMP_Text nameText;

    public void InputName()
    {
        nameText.text = inputField.text;
    }
    public void End()
    {
        NetworkStart.SetActive(true);
        Destroy(inputCanvas);
    }
    // Start is called before the first frame update
    void Start()
    {
        nameText = PlayerPrefab.GetComponentInChildren<TMP_Text>();
        nameText.text = "";
        inputField = inputCanvas.GetComponentInChildren(typeof(TMP_InputField)) as TMP_InputField;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
