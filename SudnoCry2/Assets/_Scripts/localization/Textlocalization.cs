using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textlocalization : MonoBehaviour
{
    [SerializeField]private string language;
    Text text;

    public string textRU;
    public string textEng;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        language = PlayerPrefs.GetString("language");

        if(language == "" || language == "Eng")
        {
            text.text = textEng;
        }
        else if(language == "RU")
        {
            text.text = textRU;
        }
    }
}
