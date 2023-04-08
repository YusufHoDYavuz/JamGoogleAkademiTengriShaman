using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSkill : MonoBehaviour
{
    [SerializeField] private float skillSpeed;
    [SerializeField] private int damageAmount;

    private void Update()
    {
        transform.Translate(Vector3.forward * skillSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthTEST>().health -= damageAmount;
        }
    }
}
