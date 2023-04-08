using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 2f;
    [SerializeField]
    private float runSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 250f;
    [SerializeField] Transform cameraTransform;

    private CharacterController _characterController;
    private PlayerAnimController _playerAnimController;


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
        float speed;
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3(_horizontal, 0, _vertical);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        // Run
        if (Input.GetKey(KeyCode.LeftShift) && inputMagnitude >= 0.1f)
        {
            _playerAnimController._movParam = inputMagnitude * 2;
            speed = runSpeed;
        }
        // Walk
        else if (inputMagnitude >= 0.1f)
        {
            _playerAnimController._movParam = inputMagnitude;
            speed = walkSpeed;
        }
        // Stop
        else
        {
            _playerAnimController._movParam = 0f;
            speed = 0f;
        }

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        Vector3 velocity = movementDirection * speed;
        _characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }


    private void OnApplicationFocus(bool focus)
    {
        if (focus) 
        { 
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

 


}
