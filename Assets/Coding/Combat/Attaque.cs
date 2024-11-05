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

    public string nom;
    public effect effet;
    public int power;
    public int precision;
    public int ppmax;
    public int ppact;

    public Attaque(effect ef, int pow, int pres, int pp, string nom)
    {
        ppmax = pp;
        ppact = pp;
        precision = pres;
        effet = ef;
        this.nom = nom;
    }
}
