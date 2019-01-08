using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    InputDevice ActiveController;

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


    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        PlayerCam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
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

        ActiveController = InputManager.ActiveDevice;

        ValueX = ActiveController.GetControl(InputControlType.LeftStickX);
        ValueZ = ActiveController.GetControl(InputControlType.LeftStickY);

        if (isGrounded)
        {
            VerticalVel -= 0;
        } else
        {
            VerticalVel -= 2;
        }
        MoveVector = new Vector3(0, VerticalVel, 0);
        Controller.Move(MoveVector);

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
