using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class RedPlayerInfo : MonoBehaviour
{
    //initiates the varibles for the script
    public static RedPlayerInfo instance;
    //this variable holds any text for the ...
    //... red player's info box
    public Text infoRedText;
    //initalizes any variable
    //before gameplay commences
    void Awake()
    {
        instance = this;
        //default text box is made
        infoRedText.text = "";
    }
    //this displays the sum of money that ...
    //..the red player has
    public void ShowMessage(string _text)
    {
        //the text is displayed
        infoRedText.text = _text;
    }
}
