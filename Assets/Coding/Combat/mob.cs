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

    public Attaque[] attack;

    public Sprite sprite;

    public void ResetPP()
    {
        foreach (Attaque item in attack)
        {
            if (item != null)
            {
                item.ppact = item.ppmax;
                Debug.Log(item.name);
            }
        }
    }

    public mob(type type1, type type2, int pvmax, Attaque[] attack)
    {
        this.effet = effect.None;
        Type1 = type1;
        Type2 = type2;
        this.pvmax = pvmax;
        this.attack = attack;
    }
}
