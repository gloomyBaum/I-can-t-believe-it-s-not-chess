using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class RandomEvent : RandomEventComponent
{
    private List<RandomEventComponent> onTriggerEvents;
    private List<RandomEventComponent> onExpireEvents;

    private int expiresAfter;
    private int cooldown;
    private string name;
    private string description;

    public RandomEvent(int expiresAfter, int cooldown, string name, string description)
    {
        this.OnTriggerEvents = new List<RandomEventComponent>();
        this.OnExpireEvents = new List<RandomEventComponent>();
        this.expiresAfter = expiresAfter;
        this.cooldown = cooldown;
        this.name = name;
        this.description = description;
    }

    public RandomEvent(int cooldown, string name, string description)
    {
        this.OnTriggerEvents = new List<RandomEventComponent>();
        this.OnExpireEvents = new List<RandomEventComponent>();
        this.expiresAfter = cooldown;
        this.cooldown = cooldown;
        this.name = name;
        this.description = description;
    }

    protected RandomEvent(int expiresAfter, int cooldown)
    {
        this.OnTriggerEvents = new List<RandomEventComponent>();
        this.OnExpireEvents = new List<RandomEventComponent>();
        this.expiresAfter = expiresAfter;
        this.cooldown = cooldown;
        this.name = null;
        this.description = null;
    }

    protected RandomEvent(int cooldown)
    {
        this.OnTriggerEvents = new List<RandomEventComponent>();
        this.OnExpireEvents = new List<RandomEventComponent>();
        this.expiresAfter = cooldown;
        this.cooldown = cooldown;
        this.name = null;
        this.description = null;
    }
    public int ExpiresAfter { get => expiresAfter; protected set => expiresAfter = value; }
    public int Cooldown { get => cooldown; protected set => cooldown = value; }
    public string Description { get => description; protected set => description = value; }
    public string Name { get => name; protected set => name = value; }
    public List<RandomEventComponent> OnTriggerEvents { get => onTriggerEvents; set => onTriggerEvents = value; }
    public List<RandomEventComponent> OnExpireEvents { get => onExpireEvents; set => onExpireEvents = value; }
   

    public override void OnBoardSizeUpdate(BoardRandomEvent e)
    {
        foreach(RandomEventComponent re in OnTriggerEvents)
        {
            re.OnBoardSizeUpdate(e);
        }
        foreach(RandomEventComponent re in OnExpireEvents)
        {
            re.OnBoardSizeUpdate(e);
        }
    }

    public override void OnTrigger()
    {
        foreach (RandomEventComponent re in OnTriggerEvents)
        {
            re.OnTrigger();
        }

    }

    public override void OnTurnUpdate()
    {
        foreach (RandomEventComponent re in OnTriggerEvents)
        {
            re.OnTurnUpdate();
        }
        foreach (RandomEventComponent re in OnExpireEvents)
        {
            re.OnTurnUpdate();
        }
    }

    public virtual void OnExpire() 
    {
        
        foreach (RandomEventComponent re in OnExpireEvents)
        {
            re.OnTrigger();
            if(re.GetType() == typeof(RandomEvent))
            {
                RandomEventSystem.QueueEvent(re as RandomEvent);
            }
        }


    }
}


