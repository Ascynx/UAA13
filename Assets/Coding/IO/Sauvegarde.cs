using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Sauvegarde : MonoBehaviour
{
    private FicherSauvegarde parent;

    public void SetParent(FicherSauvegarde parent)
    {
        this.parent = parent;
    }

    [SerializeField]
    private DictWrapper<string, bool> events = new();

    public void test()
    {
        events.Dictionary.Add("test", true);
        events.Dictionary.Add("test2", false);

        parent.SaveSauvegarde("1");
    }
}
