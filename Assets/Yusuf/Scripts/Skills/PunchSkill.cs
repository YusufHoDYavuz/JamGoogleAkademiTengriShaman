using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSkill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthTEST>().health -= transform.GetComponentInParent<EnemyController>().damageAmount;
        }
    }
}
