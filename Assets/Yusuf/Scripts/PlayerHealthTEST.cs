using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTEST : MonoBehaviour
{
    [SerializeField] private Text playerHealth;
    public float health;

    void Update()
    {
        playerHealth.text = health.ToString();
    }
}
