using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveEditorDelete : EventTrigger, SaveEditorButton
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    SaveEditorSlotManager _parent;

    public bool isEmptySlot = true;
    public string slot = "UNKNOWN";

    public bool interactible = false;
    public bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnClick()
    {
        if (isEmptySlot || !interactible)
        {
            return;
        }
        else
        {
            if (slot != "UNKNOWN")
            {
                Jeu.Instance.fichierSauvegarde.Data.DeleteFichier(slot);
                _parent.UpdateStatus(false);
            }
            else
            {
                Debug.LogError("Slot name is UNKNOWN");
            }
        }
    }

    public void UpdateStatus(bool slotExists, string slot)
    {
        isEmptySlot = !slotExists;
        this.slot = slot;
    }

    public void SetInteractible(bool interactible)
    {
        this.interactible = interactible;
    }

    public bool IsInteractible()
    {
        return interactible;
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

    public void FixedUpdate()
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
}
