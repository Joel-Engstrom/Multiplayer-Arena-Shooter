using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using InControl;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private InputDevice ActiveController;

    [Header("Settings")]
    public float speed = 10f;
    public float jumpStrength = 5f;
    public float lookSpeed = .5f;

    [Header("Objects & Layers")]
    public LayerMask groundLayer;
    public GameObject groundCheck;
    public GameObject head;

    private bool isGrounded = false;
    private Rigidbody rb;

    private float moveHorizontal;
    private float moveVertical;
    private Vector3 moveDirection;

    private float lookHorizontal;
    private float lookVertical;
    private Vector3 lookDirection;
    private Vector3 headDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        ActiveController = InputManager.ActiveDevice;

        moveHorizontal = ActiveController.LeftStickX;
        moveVertical = ActiveController.LeftStickY;

        lookHorizontal = ActiveController.RightStickX;
        lookVertical = ActiveController.RightStickY;

        if (Physics.CheckSphere(groundCheck.transform.position, .3f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(moveDirection * speed);

        lookDirection = new Vector3(0, lookHorizontal, 0);
        headDirection = new Vector3(lookVertical, 0, 0);
        transform.Rotate(lookDirection);
        head.transform.Rotate(headDirection);
    }
}
