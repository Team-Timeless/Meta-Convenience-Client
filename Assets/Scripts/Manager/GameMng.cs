using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    private static GameMng _Instance;

    public static GameMng I
    {
        get
        {
            if(_Instance.Equals(null))
            {
                Debug.Log("Instance is null");
            }
            return _Instance;
        }
    }

    void Awake()
    {
        _Instance = this;
    }
}
