using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SettingsManager : AbstractSubGui
{
    [SerializeField]
    private MainMenuGUI _parent;
    [SerializeField]
    private SubGUIExitButton exitButton;

    public bool open = false;

    public void LockSlots()
    {
        exitButton.SetInteractible(false);
    }

    public void UnlockSlots()
    {
        exitButton.SetInteractible(true);
    }

    public override void LoadEaseInAnimation()
    {
        Animation anim = GetComponent<Animation>();
        if (anim != null)
        {
            anim.Play("Ease In - Settings");
        }
        open = true;
    }

    public override void LoadEaseOutAnimation()
    {
        Animation anim = GetComponent<Animation>();
        if (anim != null)
        {
            anim.Play("Ease Out - Settings");
        }
        open = false;
        _parent.OnSubGuiClosed();
    }


    public sbyte currentIdx = 0;//0 = exit button, else = other settings.
    public override void OnGuiMoved(Vector2 dir)
    {
        if (dir == Vector2.up)
        {

        }
        else if (dir == Vector2.down)
        {

        }
    }

    public override void OnGuiSelect()
    {
        if (currentIdx == 0)
        {
            exitButton.OnClick();
        }
    }
}
