using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public float _movParam;
    public bool attackParam;
    public bool earthSkillParam;
    public bool airSkillParam;
    public bool waterSkillParam;
    public bool fireSkillParam;
    public bool isSwordAnimPlaying = false;
    public bool isEarthAnimPlaying = false;
    public bool isAirAnimPlaying = false;
    public bool isWaterAnimPlaying = false;
    public bool isFireAnimPlaying = false;
    public bool swordCollider = false;


    private Animator _animator;
    private float _dampTime = 0.2f;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        SetAnim();
        Debug.Log(isSwordAnimPlaying);
    }

    private void SetAnim()
    {
        _animator.SetFloat("movParam", _movParam, _dampTime, Time.deltaTime);
        _animator.SetBool("earthSkill", earthSkillParam);
        _animator.SetBool("airSkill", airSkillParam);
        _animator.SetBool("waterSkill", waterSkillParam);
        _animator.SetBool("fireSkill", fireSkillParam);

        if (attackParam) 
        {
            _animator.Play("SwordAttack");
        }
        
    }

    private void EarthAnimState()
    {
        isEarthAnimPlaying = !isEarthAnimPlaying;
    }

    private void AirAnimState()
    {
        isAirAnimPlaying = !isAirAnimPlaying;
    }

    private void WaterAnimState()
    {
        isWaterAnimPlaying = !isWaterAnimPlaying;
    }

    private void FireAnimState()
    {
        isFireAnimPlaying = !isFireAnimPlaying;
    }

    private void SwordColliderTrigger()
    {
        swordCollider = !swordCollider;
    }

    private void SwordAnimStart()
    {
        isSwordAnimPlaying = true;
    }

    private void SwordAnimStateEnd()
    {
        isSwordAnimPlaying = false;
    }

}
