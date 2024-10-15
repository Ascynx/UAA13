using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "Mobs/New Mob")]
public class mob : ScriptableObject
{
    [System.Serializable]
    public enum type
    {
        Fire,
        Elec,
        Aerial,
        Water,
        Boss,
        SemiBoss,
        None
    }
    [System.Serializable]
    public enum effect
    {
        Paralize,
        Burn,
        Toxic,
        LowerPrecision,
        LowerPower,
        LowerDef,
        None
    }


    public effect effet;

    public type Type1;
    public type Type2;

    public int pvmax;
    public int pvactuel;

    public int manamax;
    public int manaactuel;

    public Attaque attack1;
    public Attaque attack2;
    public Attaque attack3;
}
