using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseMenuGUI : AbstractGUI
{
    [SerializeField]
    SaveEditorManager saveEditorManager;

    [SerializeField]
    List<PauseMenuButton> buttons;
    private int currentIndex = 0; //0 = load, 1 = exit

    public bool active = false;

    public override bool CanBeEscaped()
    {
        return true;
    }

    public override void OnCloseGui()
    {
        gameObject.SetActive(false);
        active = false;
        currentIndex = 0;

        Jeu.Instance.playerProperties.SetAlive(true);
        Jeu.Instance.ShowControlHelp();
        OnSubGuiClosed();
        if (saveEditorManager.open)
        {
            saveEditorManager.LoadEaseOutAnimation();
        }
    }
    public override void OnOpenGui()
    {
        gameObject.SetActive(true);
        active = true;
        Jeu.Instance.playerProperties.SetAlive(false, false);
        Jeu.Instance.HideControlHelp();
    }

    public override void OnGuiMove(Vector2 dir)
    {
        if (saveEditorManager.open)
        {
            saveEditorManager.OnGuiMoved(dir);
            return;
        }

        if (!active)
        {
            return;
        }

        if (dir == Vector2.up)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
        }
        else if (dir == Vector2.down)
        {
            if (currentIndex < buttons.Count - 1)
            {
                currentIndex++;
            }
        }
    }

    public override void OnGuiSelect()
    {
        if (saveEditorManager.open)
        {
            saveEditorManager.OnGuiSelect();
            return;
        }

        if (!active)
        {
            return;
        }

        if (currentIndex < 0 || currentIndex > buttons.Count)
        {
            return;
        }

        PauseMenuButton button = buttons[currentIndex];
        if (!button.canClick)
        {
            return;
        }
        OnClicked(button.buttonName);
    }

    public void OnSelectionChanged(string name)
    {
        if (!active)
        {
            return;
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].buttonName == name)
            {
                buttons[i].selected = true;
                currentIndex = i;
#if UNITY_EDITOR
                EditorUtility.SetDirty(buttons[i]);
#endif
            }
            else
            {
                buttons[i].selected = false;
            }
        }
    }

    public void OnClicked(string name)
    {
        switch (name)
        {
            case "load":
                {
                    saveEditorManager.LoadEaseInAnimation();
                    break;
                }
            case "save":
                {
                    Jeu.Instance.fichierSauvegarde.Data.SauvegardeFichier(Jeu.Instance.fichierSauvegarde.Data.Slot);
                    break;
                }
            case "exit":
                {
                    Jeu.Instance.OpenGUI(Jeu.Instance.mainMenuGUI);
                    break;
                }
        }
    }

    public override void OnSubGuiClosed()
    {
        active = true;
        foreach (PauseMenuButton button in buttons)
        {
            button.canClick = true;
        }
    }

    public override void OnSubGuiOpen()
    {
        active = false;
        foreach (PauseMenuButton button in buttons)
        {
            button.canClick = false;
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
