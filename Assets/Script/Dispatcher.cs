using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    private static Dispatcher _instance;
    private static SynchronizationContext _syncContext;

    public static Dispatcher Instance
    {
        get { return _instance; }
    }

    public void RunOnMainThread(Action action)
    {
        _syncContext.Post(_ => action.Invoke(), null);
       
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            _syncContext = SynchronizationContext.Current;
        }
    }
}
