using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFight : EventTrigger
{
    private Image self;

    public TextMeshProUGUI NomAttaque;
    public TextMeshProUGUI PPAttaque;

    public Image equipped;

    public Combat fight;
    public int att;

    public bool left, ext;
    public bool canClick = true;
    public bool selected = false;

    private Vector3 defaultTransform;

    private void Awake()
    {
        transform.GetPositionAndRotation(out defaultTransform, out Quaternion _);
    }

    private void Start()
    {
        self = gameObject.GetComponent<Image>();
    }

    public void ResetPosition()
    {
        transform.SetPositionAndRotation(defaultTransform, transform.rotation);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnClick()
    {
        if (!canClick)
        {
            return;
        }
        fight.FightAdvence(att);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!canClick)
        {
            return;
        }
        selected = true;
    }

    //vu que c'est désactivé, on va retourner à l'état par défaut quand l'on quitte.
    private void OnDisable()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void FixedUpdate()
    {
        if (selected)
        {
            if (transform.localScale != new Vector3(1.1f, 1.1f, 1.1f))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.1f, 1.1f, 1.1f), Time.deltaTime * 10);
            }
        }
        else
        {
            if (transform.localScale != new Vector3(1f, 1f, 1f))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
            }
        }
    }

    public void SetEquippedSprite(Sprite sprite)
    {
        equipped.sprite = sprite;
        if (sprite == null)
        {
            equipped.enabled = false;
        }
        else
        {
            equipped.enabled = true;
        }
    }
}
