using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    mob ennemi;
    mob player;

    Attaque attackEnnemi;
    Attaque attackPlayer;
    void turn()
    {
        int precisionPlayer = attackPlayer.precision;
        if (player.effet == mob.effect.LowerPrecision)
        {
            precisionPlayer = int.Parse(Mathf.Round(float.Parse(precisionPlayer.ToString()) / 2).ToString);
        }
        if (Random.Range(0, 100) <= attackPlayer.precision)
        {

        }
        ennemi.pvactuel -= attackPlayer.power;
    }
}
