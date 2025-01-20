using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIControlGroupManager : MonoBehaviour
{
    private string _rawText;
    private Sprite _rawSprite;

    public TMP_Text text;
    public Image icon;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    private void OnEnable()
    {
        //re-enable the handled elements
        text.enabled = true;
        icon.enabled = true;
    }

    private void OnDisable()
    {
        //disable the handled elements
        text.enabled = false;
        icon.enabled = false;
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (text != null && _rawText != null && _rawText != text.text)
        {
            text.text = _rawText;
        }

        if (icon != null && _rawSprite != null && _rawSprite != icon.sprite)
        {
            icon.sprite = _rawSprite;
        }
    }

    public void UpdateText(string text)
    {
        _rawText = text;
    }

    public void UpdateSprite(Sprite sprite)
    {
        _rawSprite = sprite;
    }
}
