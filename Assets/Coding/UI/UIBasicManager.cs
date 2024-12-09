using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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
        //disable the ctrl UI groups by default, as they do not yet render anything.
        for (int i = 0; i < ctrlGroups.Length; i++)
        {
            ctrlGroups[i].enabled = false;
        }
        inputIntegration = ScriptableObject.FindFirstObjectByType<InputSystemIntegration>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
