using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEventSystem : MonoBehaviour
{
    public static RandomEventSystem Instance;
    public const float CAMERA_HEIGHT_FACTOR = 1.0f;
    public const float TILE_OFFSET = 0.5f;

    private System.Random randomIndexGenerator;


    private List<RandomEvent> queuedEvents;
    private Dictionary<RandomEvent, int> activeEvents; //alle aktiven Events und die turns in denen sie getriggert wurden
    private Dictionary<RandomEvent, int> onCooldownEvents;
    private List<RandomEvent> allEvents;
    private List<RandomEvent> triggerableEvents;
    private List<System.Type> triggerOnlyOnceEvents;


    private int eventProbability;
    private int turnsBetweenEvents;
    private int lastTriggerTurn;

    private RandomEvent[] nextEvent;
 

// Ui für Textanzeige
    public Text eventsText;

    public Text sizeBoardText;

    public Text eventDiscriptionText;

    public Text eventDiscHeadText;

    public Text aktualEventsText;

    public int eventTriggered = 0;

    public System.Random RandomIndexGenerator { get => randomIndexGenerator; set => randomIndexGenerator = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void OnSessionStart()
    {
        activeEvents = new Dictionary<RandomEvent, int>();
        onCooldownEvents = new Dictionary<RandomEvent, int>();

        queuedEvents = new List<RandomEvent>();

        Scenario currentScenario = SessionManager.Instance.CurrentScenario;
        allEvents = currentScenario.PossibleEvents;
        triggerableEvents = new List<RandomEvent>();
        

        foreach(RandomEvent e in allEvents)
        {
            triggerableEvents.Add(e);
            
        }

        triggerOnlyOnceEvents = new List<System.Type>();

        triggerOnlyOnceEvents.Add(typeof(ForTheKing));
        triggerOnlyOnceEvents.Add(typeof(SimpleBoardSizeEvent));
        triggerOnlyOnceEvents.Add(typeof(Baumaßnahme));


        randomIndexGenerator = new System.Random();


        //maybe start with a random number of turns until the first event?
        //turnsBetweenEvents = randomIndexGenerator.Next(1, EVENT_PROBABILITY);
        eventProbability = currentScenario.MaxEventInterval;
        turnsBetweenEvents = Instance.eventProbability;//currently: guaranteed X turns till first event

        lastTriggerTurn = 0;

        nextEvent = new RandomEvent[3];

        nextEvent[0] = Instance.NextEvent();
        nextEvent[1] = Instance.NextEvent();
        nextEvent[2] = Instance.NextEvent();

        //center camera and adjust height
        Vector3 newPos = new Vector3();
        newPos.x = currentScenario.XSize / 2 - 0.5f;
        newPos.z = currentScenario.YSize / 2 - 0.5f;
        int largerSide = Mathf.Max(currentScenario.XSize, currentScenario.YSize);
        newPos.y = CAMERA_HEIGHT_FACTOR * (largerSide);

        Camera camera = Camera.main;
        camera.transform.position = newPos;

        Debug.Log("Camera Pos on SessionStart: " + newPos);

    }
    //update wird jede Runde aufgerufen 
    public void UpdateAllEvents()
    {

        UpdateEvents();

        //Version 1: one guaranteed event in EVENT_PROBABILITY turns
        if (lastTriggerTurn + turnsBetweenEvents == SessionManager.Instance.TurnCounter)
        {
            TriggerEvent();
            ChessPieceManager.Instance.UpdateAllPossibleMoves();

            lastTriggerTurn = SessionManager.Instance.TurnCounter;
            turnsBetweenEvents = RandomIndexGenerator.Next(1, eventProbability);//1 to EVENT_PROB because events can only trigger once per turn!

            nextEvent[0] = NextEvent();
            nextEvent[1] = NextEvent();
            nextEvent[2] = NextEvent();
            
        }

        
        //Debug.Log("Possible Next Events: " + nextEvent[0].Name + ", " + nextEvent[1].Name + ", " + nextEvent[2].Name);
        string s = ("Mögliche nächste Events: " + "\n"  + nextEvent[0].Name + ", " + "\n" +  nextEvent[1].Name + ", " + "\n" + nextEvent[2].Name);
        eventsText.text = s;
        
    }

    public static void BoardSizeUpdate(BoardRandomEvent e)
    {
        //Update camera position
        Camera camera = Camera.main;

        //center camera position
        Vector3 newPos = new Vector3();
        newPos.x = ChessBoardManager.Instance.XSize/ 2 - 0.5f;
        newPos.z = ChessBoardManager.Instance.YSize / 2 - 0.5f;

        //Vector3 newPos = camera.transform.position;
        //Debug.Log("Camera: " + camera.transform.position.ToString());

        //calculate new Height
        int largerSide = Mathf.Max(ChessBoardManager.Instance.XSize, ChessBoardManager.Instance.YSize);
        float newHeight = CAMERA_HEIGHT_FACTOR * largerSide;
        newPos.y = newHeight;

        camera.transform.position = newPos;
        //Debug.Log("NewPos: " + newPos.ToString());
        //Debug.Log("New Camera Position: " + camera.transform.position.ToString());
        //Anzeige der Feldgröße
        


        //ToDo: tell all possible Events that boardSize has been changed and they need to update
        foreach (RandomEvent re in Instance.allEvents) //possibleEvents reicht weil dort ja auch die aktiven gelistet sind.
        {
            re.OnBoardSizeUpdate(e);
        }
    }


    private void UpdateEvents()
    {
        List<RandomEvent> cooledEvents = new List<RandomEvent>();
        List<RandomEvent> expiredEvents = new List<RandomEvent>();

        foreach(RandomEvent e in queuedEvents)
        {
            activeEvents.Add(e, SessionManager.Instance.TurnCounter);
        }

        queuedEvents.Clear();//clear the queue


        foreach(RandomEvent e in activeEvents.Keys)
        {
            if(SessionManager.Instance.TurnCounter - activeEvents[e] >= e.ExpiresAfter)//Event expires // >= weil falls verpasst
            {
                e.OnExpire();
                ChessPieceManager.Instance.UpdateAllPossibleMoves();
                expiredEvents.Add(e);
            }
            else //update Data
            {
                e.OnTurnUpdate();
            }
        }
        foreach(RandomEvent e in onCooldownEvents.Keys)
        {
            if (SessionManager.Instance.TurnCounter - onCooldownEvents[e] >= e.Cooldown)
            {
                cooledEvents.Add(e);

                if (allEvents.Contains(e) && (!triggerOnlyOnceEvents.Contains(e.GetType())))
                {

                    if (e.GetType() == typeof(ReplaceAllChessPiecesOfType))
                    {
                        if (((ReplaceAllChessPiecesOfType)e).TemporaryChange)
                        {
                            triggerableEvents.Add(e);
                        }
                    }
                    else
                    {
                        triggerableEvents.Add(e);
                    }

                }
            }
        }
        foreach(RandomEvent e in expiredEvents)
        {
            activeEvents.Remove(e);
        }

        foreach (RandomEvent e in cooledEvents)
        {
            onCooldownEvents.Remove(e);
        }

    }

    private void TriggerEvent()
    {
        //get next event
        //add next event to acitveEvents and remember the current turn
        //trigger the event


        int index = RandomIndexGenerator.Next(0, 2);
        RandomEvent _event = nextEvent[index];
        activeEvents.Add(_event, SessionManager.Instance.TurnCounter);
        triggerableEvents.Remove(nextEvent[index]);

        //put all Events of the same type/category on cooldown
        foreach (RandomEvent e in allEvents)
        {
            if(e.GetType() == _event.GetType() && e.GetType() != typeof(Nothing))
            {
                if (onCooldownEvents.ContainsKey(e))
                {
                    onCooldownEvents[e] = SessionManager.Instance.TurnCounter;
                }
                else
                {
                    onCooldownEvents.Add(e, SessionManager.Instance.TurnCounter);
                }
                if (triggerableEvents.Contains(e))
                {
                    triggerableEvents.Remove(e);
                }
            }
        }


        nextEvent[index].OnTrigger();
        //Debug.Log("Event Triggered:" + nextEvent[index].Name + ": " + nextEvent[index].Description);
        // Aktuelles Event anzeigen
        string r = nextEvent[index].Name;
        string s = nextEvent[index].Description;
        eventDiscHeadText.text = r;
        eventDiscriptionText.text = s;
        TriggerDiscription(1);
        aktualEventsText.text = ("Aktuelles Ereignis: " + "\n" + r);
        // Sound Event trigger
        FindObjectOfType<AudioManager1>().Play("EventHappens");
    }

    public static void TriggerDiscription(int i)
    {
        Instance.eventTriggered = i;

    }

    public static void QueueEvent(RandomEvent re)
    {
        Instance.queuedEvents.Add(re);
    }

    private RandomEvent NextEvent()
    {
        //Debug.Log(possibleEvents);

        if(triggerableEvents.Count != 0)
        {
            int randomIndex = randomIndexGenerator.Next(0, triggerableEvents.Count);
            return triggerableEvents[randomIndex];
        }
        else
        {
            return new Nothing();
        }

    }

    //gibt ein Tile zurück auf dem keine Figur steht und das nicht disabled ist
    private Tile RandomFreeTile()
    {
        //Debug.Log("Turn: " + SessionManager.Instance.TurnCounter);
        List<Tile> freeTiles = new List<Tile>();
        
        foreach(Tile t in ChessBoardManager.Instance.TileList)
        {
            if (t.TileState == Tile.State.Free)
            {
                freeTiles.Add(t);
            }
        }

        //Debug.Log("Free Tiles: " + freeTiles);
        int rndIndex = randomIndexGenerator.Next(0, freeTiles.Count);
        return freeTiles[rndIndex];
    }

    public static Vector2Int RandomFreePosition()
    {
        Vector2Int pos = new Vector2Int();
        Tile freeTile = Instance.RandomFreeTile();
        pos.x = freeTile.CurrentX;
        pos.y = freeTile.CurrentY;

        return pos;
    }


    //TODO: heir nochmal draufschauen sobald ReplaceChessPiece geschrieben ist
    /*
    private ChessPiece RandomChessPiece(MovementComponent mc) 
    {
        List<ChessPiece> allChessPieces = new List<ChessPiece>();

        foreach(ChessPiece c in ChessPieceManager.Instance().ChessPieceList)
        {   //TODO: wo genau hängen die MovementComponents dran?
            //find out if c has a component like mc

           //Component[] movementComponents = c.gameObject.GetComponents(typeof(MovementComponent));
            foreach(MovementComponent cmc in c.gameObject.GetComponents(typeof(MovementComponent)))
            {
                //check if cmc is of the same type as mc
                if(cmc.GetType() == mc.GetType())
                {
                    allChessPieces.Add(c);
                }
            }
        }

        int rndIndex = randomIndexGenerator.Next(0, allChessPieces.Count);
        return allChessPieces[rndIndex];
    }
    */

}

