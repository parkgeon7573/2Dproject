using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehavior<T> : MonoBehaviour where T : SingletonMonoBehavior<T>
{
    protected virtual void OnAwake() { }
    protected virtual void Onstart() { }
    public static T Instance { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = (T)this;
            OnAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == (T)this)
        {
            Onstart();
        }
    }

}
