using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonFight : EventTrigger
{
    public Combat fight;
    public int att;

    public bool left, ext;
    public override void OnPointerClick(PointerEventData eventData)
    {
            fight.FightAdvence(att);
    }
}
