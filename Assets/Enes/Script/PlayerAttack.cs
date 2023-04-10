using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int earthSkillBarrier = 2;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject icePrefab;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private GameObject fireSkillObject;


    [SerializeField] private float iceThrowForce = 5000f;
    [SerializeField] private float iceObjectDestoryTime = 5f;
    [SerializeField] private int _earthSkillCooldown = 25;
    [SerializeField] private int _earthSkillDuration = 15;
    [SerializeField] private int _waterSkillCooldown = 10;
    [SerializeField] private int _fireSkillCooldown = 10;

    private ParticleSystem _earthParticle;
    private ParticleSystem _airParticle;
    private PlayerAnimController _playerAnimController;
    private CapsuleCollider _capsuleCollider;
    private Rigidbody _iceRb;

    private int _leftClick = 0;
    private int _rightClick = 1;
    private int _yAxes = 1;
    private int _zAxes = 2;
    private int _holdEarthSkillCooldown;
    private int _holdEarthSkillDuration;
    private int _holdWaterSkillCooldown;
    private int _holdFireSkillCooldown;

    private float _airSkillColliderSize = 0.2f;
    private float _colliderSize = 0.9f;



    private enum AttackState
    {
        SwordAttack,
        EarthSkill,
        AirSkill,
        FireSkill,
        WaterSkill
    }

    private AttackState _attackState;

    private void Start()
    {
        _playerAnimController = GetComponent<PlayerAnimController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _earthParticle = particles.transform.GetChild(0).GetComponent<ParticleSystem>();
        _airParticle = particles.transform.GetChild(1).GetComponent<ParticleSystem>();

        SetSkillCooldowns();
    }

    private void SetSkillCooldowns()
    {
        _holdEarthSkillCooldown = _earthSkillCooldown;
        _earthSkillCooldown = 0;
        _holdEarthSkillDuration = _earthSkillDuration;
        _holdWaterSkillCooldown = _waterSkillCooldown;
        _waterSkillCooldown = 0;
        _holdFireSkillCooldown = _fireSkillCooldown;
        _fireSkillCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SwordAttack();
        EarthSkill();
        AirSkill();
        WaterSkill();
        FireSkill();

        Debug.Log("Air " + _playerAnimController.isAirAnimPlaying);
        Debug.Log("Fire " + _playerAnimController.isFireAnimPlaying);
        Debug.Log("Earth " + _playerAnimController.isEarthAnimPlaying);
        Debug.Log("Water " + _playerAnimController.isWaterAnimPlaying);
        Debug.Log("Sword " + _playerAnimController.isSwordAnimPlaying);
        
    }

   

    private void SwordAttack()
    {
        if (Input.GetMouseButtonDown(_leftClick) && !_playerAnimController.isEarthAnimPlaying && !_playerAnimController.isWaterAnimPlaying
            && !_playerAnimController.isFireAnimPlaying && !_playerAnimController.isEarthAnimPlaying) 
        {
            _playerAnimController.attackParam = true;
        }
        
        else
            _playerAnimController.attackParam = false;
    }

    private void EarthSkill()
    {
        // Set Active Earth Skill
        if (Input.GetMouseButtonDown(_rightClick) && _earthSkillCooldown == 0 && !_playerAnimController.isWaterAnimPlaying 
            && !_playerAnimController.isFireAnimPlaying && !_playerAnimController.isAirAnimPlaying && !_playerAnimController.isSwordAnimPlaying)
        {
            earthSkillBarrier = 2;
            _earthSkillCooldown = _holdEarthSkillCooldown;
            _earthSkillDuration = _holdEarthSkillDuration;
            _playerAnimController.earthSkillParam = true;
            _attackState = AttackState.EarthSkill;
            _earthParticle.Play();
            StartCoroutine(SkillCooldown(AttackState.EarthSkill));
            StartCoroutine(EarthSkillDuration());
        }

        else
           _playerAnimController.earthSkillParam = false;

        // Disable Earth Skill
        if (_earthSkillDuration == 0 || earthSkillBarrier == 0)
        {
            earthSkillBarrier = 0;
            _earthParticle.Stop();
        }
    }

    private void AirSkill()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_playerAnimController.isWaterAnimPlaying
            && !_playerAnimController.isFireAnimPlaying && !_playerAnimController.isEarthAnimPlaying && !_playerAnimController.isSwordAnimPlaying)
        {
            _playerAnimController.airSkillParam = true;
            _airParticle.Play();
        }

        else
            _playerAnimController.airSkillParam = false;

        if (_playerAnimController.isAirAnimPlaying)
        {
            _capsuleCollider.center = new Vector3(0, _airSkillColliderSize, 0);
            _capsuleCollider.direction = _zAxes;
        }

        else
        {
            _capsuleCollider.center = new Vector3(0, _colliderSize, 0);
            _capsuleCollider.direction = _yAxes;
            _airParticle.Stop();
        }
            
    }

    private void WaterSkill()
    {
        if (Input.GetKeyDown(KeyCode.R) && _waterSkillCooldown == 0 &&
            !_playerAnimController.isEarthAnimPlaying && !_playerAnimController.isFireAnimPlaying 
            && !_playerAnimController.isAirAnimPlaying && !_playerAnimController.isSwordAnimPlaying)
        {
            _waterSkillCooldown = _holdWaterSkillCooldown;
            _playerAnimController.waterSkillParam = true;
            GameObject ice = Instantiate(icePrefab, leftHandTransform.transform.position, transform.rotation);
            ice.GetComponent<Rigidbody>().AddForce(transform.forward * iceThrowForce * Time.deltaTime, ForceMode.Impulse);
            Destroy(ice, iceObjectDestoryTime);
            StartCoroutine(SkillCooldown(AttackState.WaterSkill));
        }

        else
            _playerAnimController.waterSkillParam = false;
 
    }

    private void FireSkill()
    { 
        if (Input.GetKeyDown(KeyCode.E) && _fireSkillCooldown == 0 && !_playerAnimController.isWaterAnimPlaying 
            && !_playerAnimController.isEarthAnimPlaying && !_playerAnimController.isAirAnimPlaying 
            && !_playerAnimController.isSwordAnimPlaying && !_playerAnimController.isSwordAnimPlaying)
        {
            _fireSkillCooldown = _holdFireSkillCooldown;
            _playerAnimController.fireSkillParam = true;
            StartCoroutine(SkillCooldown(AttackState.FireSkill));
            fireSkillObject.SetActive(true);
        }

        else
            _playerAnimController.fireSkillParam = false;

    }

    private IEnumerator SkillCooldown(AttackState skill)
    {
        if (skill == AttackState.EarthSkill)
        {
            while (_earthSkillCooldown > 0)
            {
                yield return new WaitForSeconds(1);
                _earthSkillCooldown -= 1;  
            }
        }

        if (skill == AttackState.WaterSkill)
        {
            while (_waterSkillCooldown > 0)
            {
                yield return new WaitForSeconds(1);
                _waterSkillCooldown -= 1;
            }
        }

        if (skill == AttackState.FireSkill)
        {
            while (_fireSkillCooldown > 0)
            {
                yield return new WaitForSeconds(1);
                _fireSkillCooldown -= 1;
            }
        }

    }

    private IEnumerator EarthSkillDuration()
    {
        while (_earthSkillDuration > 0)
        {
            yield return new WaitForSeconds(1);
            _earthSkillDuration -= 1;
        }
    }

    

    private void DisableFireSkill()
    {
        fireSkillObject.SetActive(false);
    }






}
