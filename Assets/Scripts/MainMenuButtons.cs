using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    //the player presses the button to play the game
   public void PlayGame()
    {
        //player is sent to the choose a sprite scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    //the player presses the button to view the game's instructions
    public void Instructions()
    {
        //player is sent to the instruction scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //player wishes to quit the game by pressing the quit button
    public void QuitGame()
    {
        //the application is quit
        Debug.Log("Quit!");
        Application.Quit();
    }

}
