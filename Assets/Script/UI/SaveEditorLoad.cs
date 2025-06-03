using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveEditorLoad : EventTrigger, SaveEditorButton
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

    public override void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnClick()
    {
        if (!interactible)
        {
            return;
        }

        if (isEmptySlot)
        {
            //nouvelle partie.

            //crée un nouveau slot.
            Sauvegarde tempData = Jeu.Instance.fichierSauvegarde.Data;
            Jeu.Instance.ResetData.CopyTo(ref tempData);
            Jeu.Instance.fichierSauvegarde.Data.Slot = slot;
            Dispatcher.Instance.RunOnMainThread(() =>
            { //requis pour je ne sais quelle raison (sans, l'indicateur de sauvegarde ne se reset pas correctement)
                Jeu.Instance.fichierSauvegarde.SaveSauvegarde(slot);
            });


            //charge les infos du nouveau slot (dans le cas où le jeu est déja chargé.)
            Jeu.Instance.fichierSauvegarde.Data.LoadFromData();
            //ferme le menu principal.
            Jeu.Instance.CloseGUI();
        }
        else
        {
            if (slot != "UNKNOWN")
            {
                Dispatcher.Instance.RunOnMainThread(
                    () => Jeu.Instance.fichierSauvegarde.Data.LoadFichier(slot)
                );
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
        if (isEmptySlot)
        {
            text.text = "Nouvelle partie";
        } else
        {
            text.text = "Charger";
        }
    }

    public void SetInteractible(bool interactible)
    {
        this.interactible = interactible;
    }

    public bool IsInteractible()
    {
        return interactible;
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
