using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AbstractSubGui : MonoBehaviour
{
    public abstract void OnGuiMoved(Vector2 dir);
    public abstract void OnGuiSelect();

    public abstract void LoadEaseInAnimation();
    public abstract void LoadEaseOutAnimation();
}
