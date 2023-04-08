using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTEST : MonoBehaviour
{
    [SerializeField] private Text playerHealth;
    public float health;

    private Rigidbody rb;

    void Update()
    {
        playerHealth.text = health.ToString();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AirSkill"))
        {
            StartCoroutine(AirEffect());
        }
    }

    private IEnumerator AirEffect()
    {
        rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        yield return new WaitForSeconds(1);
        rb.velocity = Vector3.zero;
    }
}
