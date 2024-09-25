using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get;private set;}
    protected abstract bool persistent { get;}

    protected virtual void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Destruyendo");
            return;
        }

        instance = this as T;
    }
}
