using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputIconsManager : ScriptableObject
{
    void Awake()
    {
        Texture[] resources = Resources.LoadAll<Texture>("Icons");
        for (int i = 0; i < resources.Length; i++)
        {
            //TODO charge les textures et donne un accès direct pour le render.
        }
    }
}
