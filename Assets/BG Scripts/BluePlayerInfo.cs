using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BluePlayerInfo : MonoBehaviour
{
    //initiates the varibles for the script
    public static BluePlayerInfo instance;
    //this variable holds any text for the ...
    //... blue player's info box
    public Text infoBlueText;
    //initalizes any variable
    //before gameplay commences
    void Awake()
    {
        instance = this;
        //default text box is made
        infoBlueText.text = "";
    }
    //this displays the sum of money that ...
    //..the bue player has
    public void ShowMessage(string _text)
    {
        //the text is displayed
        infoBlueText.text = _text;
    }
}
