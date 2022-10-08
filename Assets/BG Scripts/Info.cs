using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public static Info instance;
    //holds any text to be shown to player
    public Text infoText;
    //initalizes any variable 
    //before gameplay commences
    void Awake()
    {
        instance = this;
        //sets an empty text box
        infoText.text = "";
    }

    //ouputs any updates made to be shown to the player
    public void ShowMessage(string _text)
    {
        infoText.text = _text;
    }
}
