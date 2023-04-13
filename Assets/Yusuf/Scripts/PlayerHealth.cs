using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image playerProgress;
    public float health;

    private Rigidbody rb;

    void Update()
    {
        //playerHealth.text = health.ToString();
        rb = GetComponent<Rigidbody>();

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
            playerProgress.fillAmount = health / 100;
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
