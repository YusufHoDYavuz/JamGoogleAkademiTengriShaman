using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool triggerCollider = false;
    private PlayerAnimController _playerAnimController;
    private BoxCollider _boxCollider;

    private void Start()
    {
        _playerAnimController = GetComponent<PlayerAnimController>();
        triggerCollider = false;
    }

    // Update is called once per frame
    void Update()
    {
        SwordAttack();
        
    }

    private void SwordAttack()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            _playerAnimController.attackParam = true;
        }
        
        else
            _playerAnimController.attackParam = false;
    }

    private void SwordColliderTrigger()
    {
        triggerCollider = !triggerCollider;
    }
}
