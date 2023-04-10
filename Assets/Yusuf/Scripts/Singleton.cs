using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    public bool isDeadAir;
    public bool isDeadEarth;
    public bool isDeadLava;
    public bool isDeadWater;

    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        
    }
}
