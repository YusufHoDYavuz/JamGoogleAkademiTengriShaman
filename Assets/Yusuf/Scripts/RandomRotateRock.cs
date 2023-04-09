using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotateRock : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.Rotate(new Vector3(Random.Range(1,10), Random.Range(5, 10), Random.Range(1, 10)) * speed * Time.deltaTime);
    }
}
