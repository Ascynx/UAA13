using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObjective
{
    [SerializeField]
    private string _questId;
    [SerializeField]
    private int _step;

    public string QuestIdentifier
    {
        get { return _questId; }
    }

    public int Step
    {
        get { return _step; }
        set { _step = value; }
    }
}
