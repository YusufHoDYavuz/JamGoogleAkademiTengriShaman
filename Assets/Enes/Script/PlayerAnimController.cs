using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public float _movX = 0f;
    public float _movY = 0f;

    private Animator _animator;

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
        _animator.SetFloat("movX", _movX);
        _animator.SetFloat("movY", _movY); 
    }
    
}
