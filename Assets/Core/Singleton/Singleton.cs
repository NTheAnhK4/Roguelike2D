using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Only one " + typeof(T) + " allows to exist");
        }

        
        instance = (T)(MonoBehaviour)this;
    }
}
