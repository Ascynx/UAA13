using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static ItemData;
using static Unity.VisualScripting.Metadata;

public class UseItem : EventTrigger
{
    public ItemData item;
    public Inventory inventaire;
    public GameObject panelUse;
    public GameObject buttonUse;
    public GameObject canvas;

    private GameObject panel;
    private GameObject btn1;
    private GameObject btn2;
    private GameObject btn3;
    private Transform tmp;

    public void Start()
    {
   
    }
    public override void OnPointerClick(PointerEventData data)
    {
        foreach (Transform child in canvas.transform)
        {
            if (child.name != "Scroll View")
            {
                Destroy(child.gameObject);
            }
        }

        panel = Instantiate(panelUse, transform.position, transform.rotation);
        panel.transform.SetParent(canvas.transform);
        
        panel.transform.position = Input.mousePosition;

        btn1 = Instantiate(buttonUse, transform.position, transform.rotation);
        tmp = btn1.transform.Find("Text (TMP)");
        tmp.GetComponent<TextMeshProUGUI>().text = "Info";
        btn1.transform.SetParent(panel.transform);

        switch (item.type)  {
            case (classe.Sword):

                btn2 = Instantiate(buttonUse, transform.position, transform.rotation);
                tmp = btn1.transform.Find("Text (TMP)");
                tmp.GetComponent<TextMeshProUGUI>().text = "Équiper l'épée";
                btn2.transform.SetParent(panel.transform);
                break;
            case (classe.Shield):
                break;
            case (classe.Parchemin):
                break;
            case (classe.Relique):
                break;
            case (classe.Commun):
                break;
        }
    }

}
