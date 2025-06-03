using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubGUIExitButton : EventTrigger
{

    [SerializeField]
    public AbstractSubGui _parent;
    public bool interactible = false;

    public bool selected = false;
    public override void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
        base.OnPointerClick(eventData);
    }

    public void OnClick()
    {
        if (!interactible)
        {
            return;
        }
        _parent.LoadEaseOutAnimation();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        selected = true;
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        selected = false;
        base.OnPointerExit(eventData);
    }

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

    public void SetInteractible(bool interactible)
    {
        this.interactible = interactible;
    }
}
