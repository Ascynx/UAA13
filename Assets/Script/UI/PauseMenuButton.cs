using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuButton : EventTrigger
{
    [SerializeField]
    private PauseMenuGUI _parent;

    public bool canClick = true;
    public bool selected;

    public string buttonName;

    private void FixedUpdate()
    {
        if (selected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.1f, 1.1f, 1.1f), Time.deltaTime * 10f);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
        }
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!canClick)
        {
            return;
        }
        _parent.OnClicked(buttonName);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!canClick)
        {
            return;
        }
        ChangeSelect();
    }

    public void ChangeSelect()
    {
        if (!selected)
        {

            _parent.OnSelectionChanged(buttonName);
        }
    }
}
