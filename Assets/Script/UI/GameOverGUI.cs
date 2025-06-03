using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGUI : AbstractGUI
{
    [SerializeField]
    Canvas InteractionsCanvas;
    [SerializeField]
    Camera GameOverCamera;

    [SerializeField]
    GameOverSelection SelectionSauvegarde;
    [SerializeField]
    GameOverSelection SelectionRetourMenu;


    private bool active = false;
    private sbyte currentIndex = 0;

    public override bool CanBeEscaped()
    {
        return false;
    }
    public override void OnCloseGui()
    {
        Jeu.Instance.playerProperties.Alive = true;
        Jeu.Instance.ShowControlHelp();
        InteractionsCanvas.gameObject.SetActive(false);
        GameOverCamera.gameObject.SetActive(false);
        active = false;
    }

    public override void OnOpenGui()
    {
        Jeu.Instance.playerProperties.Alive = false;
        Jeu.Instance.HideControlHelp();
        InteractionsCanvas.gameObject.SetActive(true);
        GameOverCamera.gameObject.SetActive(true);
        active = true;
    }

    public override void OnGuiMove(Vector2 direction)
    {
        if (direction == Vector2.left)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
        } else if (direction == Vector2.right)
        {
            if (currentIndex < 1)
            {
                currentIndex++;
            }
        }
    }

    public override void OnGuiSelect()
    {
        if (currentIndex == 0 && SelectionSauvegarde.Clickable)
        {
            //rechargement de la dernière sauvegarde
            Jeu.Instance.fichierSauvegarde.Data.LoadFichier(Jeu.Instance.fichierSauvegarde.Data.Slot);
        }
        else if (currentIndex == 1 && SelectionRetourMenu.Clickable)
        {
            //retour au menu principal
            Jeu.Instance.OpenGUI(Jeu.Instance.mainMenuGUI);
        }
    }

    private void FixedUpdate()
    {
        if (!active)
        {
            return;
        }

        if (Jeu.Instance.fichierSauvegarde.Data == null)
        {
            SelectionSauvegarde.Clickable = false;
        } else
        {
            SelectionSauvegarde.Clickable = true;
        }

        if (currentIndex == 0)
        {
            SelectionSauvegarde.Selectionne(true);
            SelectionRetourMenu.Selectionne(false);
        }
        else if (currentIndex == 1)
        {
            SelectionRetourMenu.Selectionne(true);
            SelectionSauvegarde.Selectionne(false);
        }
    }

    public override void OnSubGuiClosed()
    {
    }

    public override void OnSubGuiOpen()
    {
    }
}
