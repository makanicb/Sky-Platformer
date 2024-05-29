using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OnDestroyHandler : MonoBehaviour
{
    public event Action OnDestroyed;

    void OnDestroy()
    {
        if (OnDestroyed != null)
        {
            OnDestroyed();
        }
    }
}
