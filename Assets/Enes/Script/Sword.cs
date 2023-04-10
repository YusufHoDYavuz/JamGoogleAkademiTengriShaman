using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private BoxCollider _swordCollider;
    private PlayerAnimController _playerAnimController;
    private ParticleSystem _particleSystem;

    [SerializeField] private int swordDamage;

    private void Awake()
    {
        _swordCollider = GetComponent<BoxCollider>();
        _playerAnimController = GetComponentInParent<PlayerAnimController>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }



    private void Update()
    {
        _swordCollider.enabled = _playerAnimController.swordCollider;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") )
        {
            other.GetComponent<EnemyController>().bossHealth -= swordDamage;
            _particleSystem.Play();
        }

        if (other.CompareTag("BigBoss"))
        {
            other.GetComponent<BigBossController>().bossHealth -= swordDamage;
            _particleSystem.Play();
        }

    }












}
