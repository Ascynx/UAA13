using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static ItemData;

public class UseItem : EventTrigger
{
    public ItemData item;
    public Inventory inventaire;
    public GameObject panelUse;
    public GameObject buttonUse;
    public GameObject canvas;
    public GameObject content;

    private GameObject panel;
    private GameObject btn1;
    private GameObject btn2;
    private GameObject btn3;
    private Transform tmp;

    private Transform temp;

    public void Start()
    {
   
    }
    public override void OnPointerClick(PointerEventData data)
    {
        foreach (Transform btn in content.transform)
        {
            temp = btn.Find("PanelUse(Clone)");
            if (temp != null)
            {
                Destroy(temp.gameObject);
            }
        }

        panel = Instantiate(panelUse, transform.position, transform.rotation);
        panel.transform.SetParent(this.transform);
        
        panel.transform.position = Input.mousePosition;

        btn1 = Instantiate(buttonUse, transform.position, transform.rotation);
        tmp = btn1.transform.Find("Text (TMP)");
        tmp.GetComponent<TextMeshProUGUI>().text = "Info";
        btn1.transform.SetParent(panel.transform);

        switch (item.type)  {
            case (classe.Sword):

                btn2 = Instantiate(buttonUse, transform.position, transform.rotation);
                tmp = btn2.transform.Find("Text (TMP)");
                tmp.GetComponent<TextMeshProUGUI>().text = "Équiper l'épée";
                btn2.transform.SetParent(panel.transform);
                btn2.GetComponent<EquipeItem>().objet = this.gameObject;
                btn2.GetComponent<EquipeItem>().item = item;
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
