using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Property representing the unique instance of the class implementing the Singleton
    public static T Instance { get; private set; }

    // Property checking the instanciated status of the class implementing the Singleton
    public static bool IsInitialized => Instance != null;

    // On GameObject class Awakening in Unity, associate the GameObject class to an unique instance of it in the Singleton implementation
    // If the class associate to the GameObject is already instanciate, return an error 
    protected void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a second instance of a single class.");
        }
        else
        {
            Instance = (T) this;
        }
    }

    // On GameObject being destroyed, remove the instance of the associated class implementing the Singleton
    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
