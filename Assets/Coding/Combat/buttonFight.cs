using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonFight : EventTrigger

{
    public Combat fight;
    public int att;
    // Start is called before the first frame update
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(att.ToString());
            fight.button = att;
            fight.FightAdvence();
    }
}
