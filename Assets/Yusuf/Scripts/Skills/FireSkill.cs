using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float skillSpeed;

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
