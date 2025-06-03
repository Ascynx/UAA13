using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Livre : AbstractGUI
{
    public Transform player;

    public string[] paragraphes;
    public TextMeshProUGUI textMeshPro;
    public GameObject textMesPro;

    // Update is called once per frame
    void Update()
    {
    }

    public override bool CanBeEscaped()
    {
        return true;
    }

    public override void OnCloseGui()
    {
        textMesPro.gameObject.SetActive(false);
    }

    public override void OnOpenGui()
    {
        OnLivreOpen();
    }

    public void OnLivreOpen()
    {
        if (Vector3.Distance(player.position, transform.position) < 2)
        {
            if (textMesPro.gameObject.activeInHierarchy)
            {
                textMesPro.gameObject.SetActive(false);
            }
            else
            {
                Lire();
            }
        }
    }

    public void Lire()
    {
        textMesPro.gameObject.SetActive(true);
        textMeshPro.text = "";
        foreach (var p in paragraphes)
        {
            textMeshPro.text += p + "\n\n     ";
        }

    }

    public override void OnGuiMove(Vector2 dir)
    {
    }

    public override void OnGuiSelect()
    {
    }

    public override void OnSubGuiClosed()
    {
    }

    public override void OnSubGuiOpen()
    {
    }
}
