using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveEditorSlotManager : MonoBehaviour
{
    public string slot;

    public TextMeshProUGUI slotName;
    public SaveEditorLoad loadButton;
    public SaveEditorDelete deleteButton;

    public bool selected = false;

    private void Awake()
    {
        slotName.text = "Slot " + slot + " - ";

        loadButton.slot = slot;
        deleteButton.slot = slot;
        if (Jeu.Instance.fichierSauvegarde.Data.FichierExiste(slot))
        {
            slotName.text += "Placeholder"; //remplace par genre le temps de jeu, le nom du personnage, etc...
        }
        else
        {
            slotName.text += "Aucun fichier";
            deleteButton.gameObject.SetActive(false);
        }
    }


    public void UpdateStatus()
    {
        UpdateStatus(Jeu.Instance.fichierSauvegarde.Data.FichierExiste(slot));
    }
    public void UpdateStatus(bool fileExists)
    {
        slotName.text = "Slot " + slot + " - ";
        if (fileExists)
        {
            slotName.text += "Placeholder"; //remplace par genre le temps de jeu, le nom du personnage, etc...
            deleteButton.UpdateStatus(true, slot);
            deleteButton.gameObject.SetActive(true);
            loadButton.UpdateStatus(true, slot);
        }
        else
        {
            slotName.text += "Aucun fichier";
            deleteButton.UpdateStatus(false, slot);
            deleteButton.gameObject.SetActive(false);
            loadButton.UpdateStatus(false, slot);
        }
    }

    public void LockSlot(bool guiOpen)
    {
        loadButton.SetInteractible(false);
        deleteButton.SetInteractible(false);
    }

    public void UnlockSlot(bool guiOpen)
    {
        if (!guiOpen)
        {
            //skip on ne veut quand même pas intéragir avec le menu.
            return;
        }
        loadButton.SetInteractible(true);
        deleteButton.SetInteractible(true);
    }

    public void Load()
    {
        if (loadButton.IsInteractible())
        {
            loadButton.OnClick();
        }
    }

    public void Delete()
    {
        if (deleteButton.IsInteractible())
        {
            deleteButton.OnClick();
        }
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
}
