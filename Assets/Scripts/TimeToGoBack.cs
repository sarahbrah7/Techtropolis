using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeToGoBack : MonoBehaviour
{
    //if the player presses the button to go back
    public void GoBack()
    {
        //the player is linked back to the main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    //if the player presses the button to continue to the gameplay
    public void GoAhead()
    {
        //the player is linked ot the gameplay
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
