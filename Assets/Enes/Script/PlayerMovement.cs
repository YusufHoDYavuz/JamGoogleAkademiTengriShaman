using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private CharacterController _characterController;
    private PlayerAnimController _playerAnimController;

    private Vector3 _hold;
    private float _paramIncreasePerSecond = 1f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimController = GetComponent<PlayerAnimController>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        Vector3 _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

        if (Input.GetKey(KeyCode.LeftShift) && _direction.magnitude >= 0.1f)
        {
            _direction = _direction * 2;
            SetMotion(_direction);
        }

        else if (_direction.magnitude >= 0.1f )
        {
            SetMotion(_direction);
        }

        else
        {
            _playerAnimController._movX = 0f;
            _playerAnimController._movY = 0f;
        }
    }

    private void SetMotion(Vector3 _direction)
    {
        _characterController.Move(_direction * speed * Time.deltaTime);
        _playerAnimController._movX = _direction.x;
        _playerAnimController._movY = _direction.z;
    }




}
