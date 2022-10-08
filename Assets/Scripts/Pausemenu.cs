using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    //the game pause is orignally set to false
    public static bool GameIsPaused = false;
    //stores the pause button
    public GameObject pauseButton;
    //stores the pause menu screen
    public GameObject pauseMenuUI;
    //stores the other screen used throughout gameplay
    public GameObject otherCanvas;
   
    //initalizes whenever the gameplay updates
    void Update()
    {
        //if the player has pressd pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //the pause menu is shown and the other canvas is hidden
            pauseMenuUI.SetActive(true);
            otherCanvas.SetActive(false);
            //the gameplay has been paused
            Pause();
           
        }

        //else
        //{
        //    otherCanvas.SetActive(true);
        //    pauseMenuUI.SetActive(false);
        //}
            
    }
    //resumes the gameplay from paused state
    void Resume()
    {
        //pause menu is not shown anymore
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        //the game is no longer paused
        GameIsPaused = false;
    }
    //the gameplay is paused
    void Pause()
    {
        //pause menu is shown 
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        ///the game is now paused
        GameIsPaused = true;
    }
    //the player chooses to go back to the main menu
    public void LoadMenu()
    {
        Debug.Log("Loading Menu .....");
        //player is linked back to the main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
    //the player chooses to quit the game 
    public void QuitGame()
    {
        Debug.Log("Quiting Game ......");
        //the application itself is quit
        Application.Quit();
    }
}
