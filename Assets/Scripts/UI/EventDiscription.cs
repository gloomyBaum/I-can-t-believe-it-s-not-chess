using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventDiscription : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static bool GamePaused = false;
    public GameObject eventsDiscription;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)  || Input.GetKeyDown(KeyCode.Space))
        {
            RandomEventSystem.Instance.eventTriggered = 0;
            FindObjectOfType<AudioManager1>().StopPlaying("EventHappens");
            SetOff();
        }
        else
        {
            ShowOn();
        }
    }

    public void ShowOn()
    {
        if(RandomEventSystem.Instance.eventTriggered == 1)
        {
           GamePaused = true;
           eventsDiscription.SetActive(true);

        }
        
        
    }

    public void SetOff()
    {
        GamePaused = false;
        eventsDiscription.SetActive(false);
        foreach(Tile tile in ChessBoardManager.Instance.TileList)
        {
            tile.CancelMouseOverHighlight();
        }
    }
}
