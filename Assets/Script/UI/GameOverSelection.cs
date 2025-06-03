using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSelection : MonoBehaviour
{
    [SerializeField]
    Image indicateurSelection;
    [SerializeField]
    TMPro.TextMeshProUGUI texteBouton;

    private bool clickable = true;

    public bool Clickable { get { return clickable; } set
        {
            clickable = value;

            if (clickable)
            {
                texteBouton.color = new Color(1, 1, 1, 1);
            }
            else
            {
                texteBouton.color = new Color(1, 1, 1, 0.5f);
            }
        }
    }

    public void Selectionne(bool selectionne)
    {
        if (selectionne)
        {
            indicateurSelection.gameObject.SetActive(true);
        }
        else
        {
            indicateurSelection.gameObject.SetActive(false);
        }
    }
}
