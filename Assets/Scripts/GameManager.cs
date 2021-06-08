using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Scenario currentScenario;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        DontDestroyOnLoad(this);
        currentScenario = new StandardGame();
        //<SoundMenu
        FindObjectOfType<AudioManager1>().Play("MenuMusic");
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("MainScene");
        FindObjectOfType<AudioManager1>().StopPlaying("MenuMusic");
        FindObjectOfType<AudioManager1>().Play("BackgroundMusic");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScenario(string name)
    {
        //hier weitere scenarien einfügen
        switch (name)
        {
            case "See":
                currentScenario = new LakeScenario();
                break;
            case "Festung":
                currentScenario = new CastleScenario();
                break;
            case "Chaos":
                currentScenario = new ManyEvents();
                break;
            case "DebugScenario":
                currentScenario = new DebugScenario();
                break;
            case "Apokalypse":
                currentScenario = new ApocalypseScenario();
                break;
            case "Schach":
                currentScenario = new PlainBoringNormalChess();
                break;
            default:
                currentScenario = new StandardGame();
                break;
        }
    }
}
