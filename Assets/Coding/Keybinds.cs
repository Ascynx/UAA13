using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Keybinds
{
    private static KeyControl _up = Keyboard.current.upArrowKey;
    private static KeyControl _down = Keyboard.current.downArrowKey;
    private static KeyControl _left = Keyboard.current.leftArrowKey;
    private static KeyControl _right = Keyboard.current.rightArrowKey;
    private static KeyControl _interact = Keyboard.current.eKey;
    private static KeyControl _inventory = Keyboard.current.iKey;
    private static KeyControl _run = Keyboard.current.numpad0Key;


    public static KeyControl Up
    {
        get
        {
            return _up;
        }
    }
    public static KeyControl Down
    {
        get
        {
            return _down;
        }
    }
    public static KeyControl Left
    {
        get
        {
            return _left;
        }
    }
    public static KeyControl Right
    {
        get
        {
            return _right;
        }
    }
    public static KeyControl Interact
    {
        get
        {
            return _interact;
        }
    }
    public static KeyControl Inventaire
    {
        get
        {
            return _inventory;
        }
    }
    public static KeyControl Courir
    {
        get
        {
            return _run;
        }
    }
}
