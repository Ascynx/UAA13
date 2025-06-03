using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoutonMenuPrincipal : EventTrigger
{
    [SerializeField] private GameObject mainMenu;
    public string buttonName;

    public bool canClick = true;
    public bool selected = false;

    private void FixedUpdate()
    {
        if (selected)
        {
            if (transform.localScale != new Vector3(1.1f, 1.1f, 1.1f))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.1f, 1.1f, 1.1f), Time.deltaTime * 10);
            }
        }
        else
        {
            if (transform.localScale != new Vector3(1f, 1f, 1f))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
            }
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!canClick)
        {
            return;
        }
        // Handle button click
        mainMenu.SendMessage("OnClicked", buttonName);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!canClick)
        {
            return;
        }
        // Handle pointer enter
        ChangeSelect();
    }

    public void ChangeSelect()
    {
       if (!selected)
        {

            mainMenu.GetComponent<MainMenuGUI>().OnChangeSelection(buttonName);
        }
    }
}
