using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private BoxCollider _swordCollider;
    private PlayerAttack _playerAttack;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _swordCollider = GetComponent<BoxCollider>();
        _playerAttack = GetComponentInParent<PlayerAttack>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }



    private void Update()
    {
        _swordCollider.enabled = _playerAttack.triggerCollider;
        if (_swordCollider.enabled ) {}
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") )
        {
            _particleSystem.Play();
            Debug.Log("How many times");
        }

    }












}
