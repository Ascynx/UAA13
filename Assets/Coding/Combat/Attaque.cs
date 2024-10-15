using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attaque", menuName = "Attaques/New Attaque")]
public class Attaque : ScriptableObject
{
    [System.Serializable]
    public enum effect
    {
        Paralize,
        Burn,
        Toxic,
        PowerWind,
        LowerPrecision,
        LowerPower,
        LowerDef,
        None
    }


    public effect effet;
    public int power;
    public int costmana;
    public int precision;
    
}
