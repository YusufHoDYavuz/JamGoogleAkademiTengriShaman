using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    public bool isDeadAir;
    public bool isDeadEarth;
    public bool isDeadFire;
    public bool isDeadWater;

    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
