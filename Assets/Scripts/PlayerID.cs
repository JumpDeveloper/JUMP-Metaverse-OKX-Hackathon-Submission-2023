using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerID : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = PlayerPrefs.GetString("Account");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
