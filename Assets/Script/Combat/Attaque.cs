using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Attaques", menuName = "Attaques/New Attaque")]
public class Attaque : ScriptableObject
{
    [System.Serializable]
    public enum effect
    {
        None,
        Paralize,
        Burn,
        Toxic,
        PowerWind,
        LowerPrecision,
        LowerPower,
        LowerDef,
    }

    public string nom;
    public effect effet;
    public int power;
    public int precision;
    public int ppmax;
    public int ppact;

    public Sprite sprite;

    public Attaque(effect ef, int pow, int pres, int pp, string nom, Sprite sprite)
    {
        effet = ef;
        power = pow;
        precision = pres;
        ppmax = pp;
        ppact = pp;
        this.nom = nom;
        this.sprite = sprite;
    }

    public Attaque() { }

    public static Attaque CreateNew(effect ef, int pow, int pres, int pp, string nom, Sprite sprite)
    {
        Attaque atq = ScriptableObject.CreateInstance<Attaque>();
        atq.nom = nom;
        atq.ppmax = pp;
        atq.ppact = pp;

        atq.effet = ef;
        atq.power = pow;
        atq.precision = pres;
        atq.sprite = sprite;
        return atq;
    }

    /// <summary>
    /// Crée une nouvelle instance de l'attaque Lutte.
    /// </summary>
    public static Attaque Lutte
    {
        get
        {
            return CreateNew(Attaque.effect.None, 10, 99, 1, "Lutte", null);
        }
    }
}