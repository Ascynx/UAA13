using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMob : MonoBehaviour
{
    public mob player;
    public Inventory inventory;
    public void UpdateMob()
    {
        Attaque[] attack = new Attaque[4];
        if (!(inventory.sword is null))
        {
            attack[0] = inventory.sword.attaque;
        }
        if (!(inventory.parchemin1 is null))
        {
            attack[1] = inventory.parchemin1.attaque;
        }
        if (!(inventory.parchemin2 is null))
        {
            attack[2] = inventory.parchemin2.attaque;
        }
        if (!(inventory.parchemin3 is null))
        {
            attack[3] = inventory.parchemin3.attaque;
        }

        player = new mob(mob.type.None, mob.type.None, 100, attack);
    }
}
