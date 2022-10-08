using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    //if the player preses to go back to the main menu
    public void MenuBack()
    {
        //the player woudl be taken back ot the main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
