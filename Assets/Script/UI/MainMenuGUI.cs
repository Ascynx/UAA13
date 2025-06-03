using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class MainMenuGUI : AbstractGUI
{
    [SerializeField]
    Canvas Canvas;
    [SerializeField]
    Camera Camera;

    [SerializeField]
    List<BoutonMenuPrincipal> buttons;
    [SerializeField]
    SaveEditorManager saveEditorManager;
    [SerializeField]
    SettingsManager settingsManager;

    private bool active = false;
    [SerializeField]
    private sbyte currentIndex = 0;

    public override bool CanBeEscaped()
    {
        return false;
    }

    public override void OnCloseGui()
    {
        Jeu.Instance.playerProperties.Alive = true;
        Jeu.Instance.ShowControlHelp();
        Canvas.gameObject.SetActive(false);
        Camera.gameObject.SetActive(false);
        active = false;


        OnSubGuiClosed();
        if (saveEditorManager.open)
        {
            saveEditorManager.LoadEaseOutAnimation();
        } else if (settingsManager.open)
        {
            settingsManager.LoadEaseOutAnimation();
        }
    }

    public override void OnOpenGui()
    {
        Jeu.Instance.playerProperties.Alive = false;
        Jeu.Instance.HideControlHelp();
        Canvas.gameObject.SetActive(true);
        Camera.gameObject.SetActive(true);
        active = true;
    }

    public override void OnGuiMove(Vector2 direction)
    {
        if (saveEditorManager.open)
        {
            saveEditorManager.OnGuiMoved(direction);
            return;
        }
        if (settingsManager.open)
        {
            settingsManager.OnGuiMoved(direction);
            return;
        }


        if (direction == Vector2.up)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
        }
        else if (direction == Vector2.down)
        {
            if (currentIndex < buttons.Count - 1)
            {
                currentIndex++;
            }
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public override void OnGuiSelect()
    {
        if (saveEditorManager.open)
        {
            saveEditorManager.OnGuiSelect();
            return;
        }
        if (settingsManager.open)
        {
            settingsManager.OnGuiSelect();
            return;
        }

        if (!buttons[currentIndex].canClick)
        {
            return;
        }
        string name = buttons[currentIndex].buttonName;
        OnClicked(name);
    }

    public override void OnSubGuiClosed()
    {
        foreach (BoutonMenuPrincipal button in buttons)
        {
            button.canClick = true;
        }
    }

    public override void OnSubGuiOpen()
    {
        foreach (BoutonMenuPrincipal button in buttons)
        {
            button.canClick = false;
        }
    }

    public void OnChangeSelection(string name)
    {
        switch (name)
        {
            case "load":
                {
                    currentIndex = 0;
                    break;
                }
            case "exit":
                {
                    currentIndex = 1;
                    break;
                }
            case "settings":
                {
                    currentIndex = 2;
                    break;
                }
        }

        foreach (BoutonMenuPrincipal button in buttons)
        {
            if (button.buttonName != name)
            {
                button.selected = false;
            }
            else
            {
                button.selected = true;
            }
        }
    }

    public void OnClicked(string name)
    {
        switch(name)
        {
            case "load":
                {
                    //animation pour déplacer le SaveEditorManager en avant et désactivant les boutons en dessous.
                    saveEditorManager.LoadEaseInAnimation();

                    OnSubGuiOpen();
                    break;
                }
            case "exit":
                {
                    Application.Quit();
                    break;
                }
            case "settings":
                {
                    settingsManager.LoadEaseInAnimation();
                    OnSubGuiOpen();
                    break;
                }
        }
    }

    private void FixedUpdate()
    {
        if (!active)
        {
            return;
        }

        buttons[currentIndex].selected = true;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i != currentIndex)
            {
                buttons[i].selected = false;
            }
        }
    }
}
