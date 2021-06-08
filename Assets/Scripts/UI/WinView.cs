using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winWhite;
    public GameObject winBlack;
    public Text winTextWhite;
    public Text winTextBlack;

    private bool ready = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && ready)
        {
            CancelAndQuit();
        }
    }
    public void ShowWhiteWin()
    {
        winWhite.SetActive(true);
        string s = ("Glückwunsch!" + "\n" + "Weiß gewinnt!");
        //winText.color
        winTextWhite.text = s;

        FindObjectOfType<AudioManager1>().StopPlaying("BackgroundMusic");
        FindObjectOfType<AudioManager1>().Play("JubelSound");
        ready = true;
    }
    public void ShowBlackWin()
    {
        winBlack.SetActive(true);
        string s = ("Gratulation!" + "\n" + "Schwarz gewinnt!");
        winTextBlack.text = s;
        FindObjectOfType<AudioManager1>().StopPlaying("BackgroundMusic");
        FindObjectOfType<AudioManager1>().Play("JubelSound");
        ready = true;
    }

    private void CancelAndQuit()
    {
        winBlack.SetActive(false);
        winWhite.SetActive(false);
        ready = false;
        FindObjectOfType<AudioManager1>().StopPlaying("JubelSound");
        SceneManager.LoadScene("StartMenu");
    }
    } 

