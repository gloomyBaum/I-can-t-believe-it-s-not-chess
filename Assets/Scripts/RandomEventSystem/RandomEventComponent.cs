using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class RandomEventComponent
{
    public RandomEventComponent()
    {
    }

    public abstract void OnTrigger();

    public abstract void OnTurnUpdate();

    public abstract void OnBoardSizeUpdate(BoardRandomEvent e);

    
}
