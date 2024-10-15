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
        int i;
        if (int.TryParse(objet.name[7].ToString(), out i))
        {
            switch (item.type)
            {
                case (classe.Sword):
                    if (inventory.sword == null)
                    {
                        inventory.sword = item;
                        inventory.inventory.RemoveAt(i);
                    }

                    break;
                case (classe.Shield):
                    if (inventory.shield == null)
                    {
                        inventory.shield = item;
                        inventory.inventory.RemoveAt(i);
                    }
                    break;
                case (classe.Parchemin):
                    if (inventory.parchemin1 == null)
                    {
                        inventory.parchemin1 = item;
                        inventory.inventory.RemoveAt(i);
                    }
                    else if (inventory.parchemin2 == null)
                    {
                        inventory.parchemin2 = item;
                        inventory.inventory.RemoveAt(i);
                    }
                    else if (inventory.parchemin3 == null)
                    {
                        inventory.parchemin3 = item;
                        inventory.inventory.RemoveAt(i);
                    }
                    break;
                case (classe.Relique):
                    if (inventory.Relique == null)
                    {
                        inventory.Relique = item;
                        inventory.inventory.RemoveAt(i);
                    }
                    break;
            }
        }
        else
        {
            switch (item.type)
            {
                case (classe.Sword):
                    inventory.inventory.Add(item);
                    inventory.sword = null;
                    break;
                case (classe.Shield):
                    inventory.inventory.Add(item);
                    inventory.shield = null;
                    break;
                case (classe.Parchemin):
                    if (inventory.parchemin3 == item)
                    {
                        inventory.inventory.Add(item);
                        inventory.parchemin3 = null;
                    }
                    else if (inventory.parchemin2 == item)
                    {
                        inventory.inventory.Add(item);
                        inventory.parchemin2 = null;
                    }
                    else if (inventory.parchemin1 == item)
                    {
                        inventory.inventory.Add(item);
                        inventory.parchemin1 = null;
                    }
                    break;
                case (classe.Relique):
                    inventory.inventory.Add(item);
                    inventory.Relique = null;
                    break;
            }
        }
        inventory.Fire();
    }
}
