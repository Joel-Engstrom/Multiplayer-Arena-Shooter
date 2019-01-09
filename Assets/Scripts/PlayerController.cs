using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.Networking;
using System;

public class PlayerController : NetworkBehaviour
{
    CharacterInputController CharacterInputs;

    public float Speed;

    public LayerMask GroundLayer;
    public GameObject GroundCheck;

    private CharacterController Controller;
    private Camera PlayerCam;
    private float ValueX;
    private float ValueZ;
    private bool isGrounded;
    private Vector3 DesiredMoveDirection;
    private bool BlockRotationPlayer;
    private bool BlockRotationSpeed;
    public float DesiredRotationSpeed;
    public float AllowPlayerRotation;
    private Vector3 MoveVector;
    private float VerticalVel;

    private void Start()
    {
        CharacterInputs = new CharacterInputController();

        CharacterInputs.Left.AddDefaultBinding(Key.LeftArrow);
        CharacterInputs.Right.AddDefaultBinding(InputControlType.LeftStickY);

        CharacterInputs.MoveVertical.AddDefaultBinding(Key.RightArrow);
        CharacterInputs.MoveVertical.AddDefaultBinding(InputControlType.LeftStickY);

        CharacterInputs.Jump.AddDefaultBinding(Key.Space);
        CharacterInputs.Jump.AddDefaultBinding(InputControlType.Action1);

        CharacterInputs.Shoot.AddDefaultBinding(Mouse.LeftButton);
        CharacterInputs.Shoot.AddDefaultBinding(InputControlType.RightTrigger);

        CharacterInputs.LookX.AddDefaultBinding(Mouse.PositiveX);
        CharacterInputs.LookX.AddDefaultBinding(Mouse.NegativeX);
        CharacterInputs.LookX.AddDefaultBinding(InputControlType.RightStickX);

        CharacterInputs.LookY.AddDefaultBinding(Mouse.PositiveY);
        CharacterInputs.LookY.AddDefaultBinding(Mouse.NegativeY);
        CharacterInputs.LookY.AddDefaultBinding(InputControlType.RightStickY);


        Controller = GetComponent<CharacterController>();
        PlayerCam = transform.GetChild(1).gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (CharacterInputs.Jump.WasPressed)
        {
            PerformJump();
        }

        if (true)
        {

        }

        InputMagnitude();

        if (Physics.CheckSphere(GroundCheck.transform.position, .3f, GroundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        isGrounded = Controller.isGrounded;

        if (isGrounded)
        {
            VerticalVel = 0;
        } else
        {
            VerticalVel -= 2;
        }
        MoveVector = new Vector3(0, VerticalVel, 0);
        Controller.Move(MoveVector);

    }

    private void PerformJump()
    {
        throw new NotImplementedException();
    }

    private void PlayerAndMoveRotation()
    {
        var forward = PlayerCam.transform.forward;
        var right = PlayerCam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        DesiredMoveDirection = forward * ValueZ + right * ValueX;

        if (BlockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(DesiredMoveDirection), DesiredRotationSpeed);
        }
    }

    private void InputMagnitude()
    {
        Speed = new Vector2(ValueX, ValueZ).sqrMagnitude;

        if (Speed > AllowPlayerRotation)
        {
            PlayerAndMoveRotation();
        }
    }
}
