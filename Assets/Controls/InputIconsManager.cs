using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.U2D;

public class InputIconsManager : ScriptableObject
{

    [SerializeField] private SpriteAtlas _atlas;
    void Awake()
    {
    }

    private Sprite GetSprite(string name)
    {
        return _atlas.GetSprite(name);
    }
}
