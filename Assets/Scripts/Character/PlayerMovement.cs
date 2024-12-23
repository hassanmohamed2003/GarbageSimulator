using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 10;
    PlayerInput _Input;
    Vector2 _Movement;
    Rigidbody2D _rb;

    private void Awake()
    {
        _Input = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _Input.Enable();

        _Input.Gameplay.Movement.performed += onMovement;
        _Input.Gameplay.Movement.canceled += onMovement;
    }

    private void OnDisable()
    {
        _Input.Disable();
    }

    private void onMovement(InputAction.CallbackContext context)
    {
        _Movement = context.ReadValue<Vector2>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _Movement * _playerSpeed;
    }
}
