using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.GameplayActions InputActions;

    private PlayerMotor playerMotor;
    private PlayerLookAt PlayerLookAt;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        InputActions = playerInput.Gameplay;
        playerMotor = GetComponent<PlayerMotor>();
        PlayerLookAt = GetComponent<PlayerLookAt>();
        InputActions.Jump.performed += ctx => playerMotor.Jump(); 
        InputActions.Crouch.performed += ctx => playerMotor.Crouch();
        InputActions.Sprint.performed += ctx => playerMotor.Sprint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMotor.ProcessMove(InputActions.Movement.ReadValue<Vector2>());
        

    }

    private void LateUpdate()
    {
        PlayerLookAt.ProcessLook(InputActions.LookAt.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        InputActions.Enable();

    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}
