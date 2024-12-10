using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using static InputIconsManager;

public class UIBasicManager : MonoBehaviour
{
    public UIControlGroupManager[] ctrlGroups;
    private InputSystemIntegration inputIntegration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
        inputIntegration = ScriptableObject.FindFirstObjectByType<InputSystemIntegration>();
    }

    bool done = false;
    // Update is called once per frame
    void Update()
    {
        if (inputIntegration != null && !done)
        {
            InputIconsManager manager = inputIntegration.GetIconsManager();

            Debug.Log(manager.FindIconTex("X", "X", "Color", null, out Sprite sprite));
            ctrlGroups[0].UpdateSprite(sprite);
            ctrlGroups[0].UpdateText("X X");
            Debug.Log(manager.FindIconTex("Keyboard", "A", out Sprite KAsprite));
            ctrlGroups[1].UpdateSprite(KAsprite);
            ctrlGroups[1].UpdateText("Keyboard A");
            Debug.Log(manager.FindIconTex("S", "Dpad", "X", null, out Sprite SXsprite));
            ctrlGroups[2].UpdateSprite(SXsprite);
            ctrlGroups[2].UpdateText("S DPad X");
            done = true;
        }
    }
}
