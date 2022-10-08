using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePref : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //saves the players sum of money and dice outputs
        PlayerPrefs.SetInt("DiceRolled", 0);
        PlayerPrefs.SetInt("RedMoney", 0);
        PlayerPrefs.SetInt("BlueMoney", 0);

    }
 
}
