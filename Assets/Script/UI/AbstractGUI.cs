using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGUI : MonoBehaviour
{
    public abstract void OnCloseGui();
    public abstract void OnOpenGui();

    public abstract void OnGuiMove(Vector2 dir);

    public abstract void OnGuiSelect();

    public abstract bool CanBeEscaped();

    public abstract void OnSubGuiClosed();
    public abstract void OnSubGuiOpen();
}
