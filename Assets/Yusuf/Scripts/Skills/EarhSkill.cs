using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarhSkill : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float downDelay;
    [SerializeField] private int damageAmount;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(EarthEffect());
    }

    private IEnumerator EarthEffect()
    {
        rb.AddForce(Vector3.up * speed + transform.forward * speed, ForceMode.Impulse);
        yield return new WaitForSeconds(downDelay);
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthTEST>().health -= damageAmount;
            Destroy(gameObject);
        }
    }
}
