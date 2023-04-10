using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSkill : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().health -= damageAmount;
        }
    }
}
