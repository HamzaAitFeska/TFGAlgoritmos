using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playervelocity;
    public float speed = 5.0f;
    private bool isGrounded, crouching, sprinting;
    private float crouchTimer = 0f;
    private bool lerpCrouch;
    public float Gravity = -9.8f;
    public float JumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playervelocity.y += Gravity * Time.deltaTime;
        if(isGrounded && playervelocity.y < 0)
        {
            playervelocity.y = -1f;
        }

        characterController.Move(playervelocity * Time.deltaTime);

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;

            if (crouching)
            {
                characterController.height = Mathf.Lerp(characterController.height, 1, p);
            }
            else
            {
                characterController.height = Mathf.Lerp(characterController.height, 2, p);
            }

            if(p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

    }

    public void Jump()
    {
        if (isGrounded)
        {
            playervelocity.y = Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8;
        }
        else
        {
            speed = 5;
        }
    }
}
