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

    public void Update()
    {
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.parent.parent.parent.GetComponent<RectTransform>().sizeDelta.x / 4, transform.GetComponent<RectTransform>().sizeDelta.y);
        if (left)
        {
            transform.localPosition = new Vector3(transform.parent.parent.parent.GetComponent<RectTransform>().sizeDelta.x * 0.125f, 0, 0);
        } 
        else
        {
            transform.localPosition = new Vector3(transform.parent.parent.parent.GetComponent<RectTransform>().sizeDelta.x * -0.125f, 0, 0);
        }
        if (ext)
        {
            transform.localPosition = transform.localPosition * 3;
        }
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
            fight.FightAdvence(att);
    }
}
