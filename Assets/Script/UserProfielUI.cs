using PlayFab.EconomyModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Microsoft.Unity.VisualStudio.Editor;
using Image = UnityEngine.UI.Image;

public class UserProfielUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image profielImage;
    public void SetProfiel(string name,Sprite playerIcon )
    {
        nameText.text = name;
        profielImage.sprite = playerIcon;
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
