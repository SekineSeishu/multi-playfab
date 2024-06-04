using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string id;
    public Sprite icon;
    public string _name;
    public string _rank;
    public string _exp;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private Image playerImage;
    // Start is called before the first frame update
    void Start()
    {
        playerName.text = _name;
        playerImage.sprite = icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
