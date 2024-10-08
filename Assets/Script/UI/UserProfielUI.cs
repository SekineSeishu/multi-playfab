using PlayFab.EconomyModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UserProfielUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;//プレイヤー名の表示
    [SerializeField] private Image profielImage;//プレイヤーアイコンの表示

    //プレイヤー情報をUIにセットする
    public void SetProfiel(string name,Sprite playerIcon)
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
