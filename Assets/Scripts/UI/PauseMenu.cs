using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
   
    private GameObject Chessboard;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }

            
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
         pauseMenuUI.SetActive(false);
         GameIsPaused = false;
        // Sound Active
       //  FindObjectOfType<AudioManager1>().Play("BackgroundMusic");
    //  WinView.GamePaused = true;
     // Debug.Log("" + WinView.GamePaused);
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        //Sound paused
       // FindObjectOfType<AudioManager1>().StopPlaying("BackgroundMusic");
    }
   

    
    public void Quit()
    {
        GameIsPaused = false;
        GameManager.Instance.currentScenario = new StandardGame();
        FindObjectOfType<AudioManager1>().Play("MenuMusic");
        FindObjectOfType<AudioManager1>().StopPlaying("BackgroundMusic");
        SceneManager.LoadScene("StartMenu");

    }
}
