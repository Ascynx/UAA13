using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static ItemData;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.Rendering.DebugUI;

public class EquipeItem : EventTrigger
{
    public GameObject objet;
    public ItemData item;
    public Inventory inventory;
    public override void OnPointerClick(PointerEventData data)
    {
        ItemData PlaceHolder;
        int i = int.Parse(objet.name[7].ToString());
            switch (item.GetType().ToString())
            {
                case ("Sword"):
                    if (inventory.sword == null)
                    {
                        PlaceHolder = inventory.sword;
                        inventory.sword = (Sword)item;
                        inventory.inventory[i] = PlaceHolder;
                    }

                    break;
                case ("Shield"):
                    if (inventory.shield == null)
                    {
                        PlaceHolder = inventory.shield;
                        inventory.shield = (Shield)item;
                        inventory.inventory[i] = PlaceHolder;
                    }
                    break;
                case ("Parchemin"):
                    if (inventory.parchemin1 == null)
                    {
                        PlaceHolder = inventory.parchemin1;
                        inventory.parchemin1 = (Parchemin)item;
                        inventory.inventory[i] = PlaceHolder;
                    }
                    else if (inventory.parchemin2 == null)
                    {
                        PlaceHolder = inventory.parchemin2;
                        inventory.parchemin2 = (Parchemin)item;
                        inventory.inventory[i] = PlaceHolder;
                    }
                    else if (inventory.parchemin3 == null)
                    {
                        PlaceHolder = inventory.parchemin3;
                        inventory.parchemin3 = (Parchemin)item;
                        inventory.inventory[i] = PlaceHolder;
                    }
                    break;
                case ("Relique"):
                    if (inventory.relique == null)
                    {
                        PlaceHolder = inventory.relique;
                        inventory.relique = (Relique)item;
                        inventory.inventory[i] = PlaceHolder;
                    }
                    break;
            }
        inventory.Fire();
    }
}
