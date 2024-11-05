using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonFight : MonoBehaviour

{
    public Combat fight;
    public int att;
    // Start is called before the first frame update
    public void OnClick(PointerEventData eventData)
    {
        if (fight.playerTurn)
        {
            fight.button = att;
            fight.playerTurn = false;
        }
    }
}
