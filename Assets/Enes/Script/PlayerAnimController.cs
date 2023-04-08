using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public float _movParam;
    public bool attackParam;
    public bool mov;

    private Animator _animator;
    private float _dampTime = 0.2f;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnim();
    }

    private void SetAnim()
    {
        _animator.SetFloat("movParam", _movParam, _dampTime, Time.deltaTime);
        _animator.SetBool("attackParam", attackParam);

        if (attackParam) { _animator.Play("SwordAttack"); }
        
    }
    
}
