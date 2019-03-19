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
    public float lookSpeed = 2f;
    public float strafeVar = 0.1f;

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

    private float downForce = 0f;

    private bool useController = false;

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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ActiveController = InputManager.ActiveDevice;

        if (Input.GetKeyDown(KeyCode.F9))
        {
            useController = true;
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            useController = false;
        }

        if (useController)
        {
            moveHorizontal = ActiveController.LeftStickX;
            moveVertical = ActiveController.LeftStickY;

            lookHorizontal = ActiveController.RightStickX;
            lookVertical = ActiveController.RightStickY;
        } else
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = -Input.GetAxisRaw("Vertical");

            lookHorizontal = Input.GetAxisRaw("Mouse X");
            lookVertical = -Input.GetAxisRaw("Mouse Y");
        }

        if (Physics.CheckSphere(groundCheck.transform.position, .5f, groundLayer))
        {
            isGrounded = true;
            downForce = 0f;
        }
        else
        {
            isGrounded = false;
            rb.AddForce(new Vector3(0,0,downForce += Time.deltaTime * 2f));
        }
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        //På Marken
        if (isGrounded)
        {
            rb.AddForce(transform.right * speed * moveHorizontal);
            rb.AddForce(transform.forward * speed * -moveVertical);
        }
        else //I luften (Strafea)
        {
            rb.AddForce(transform.right * speed * moveHorizontal * strafeVar);
            rb.AddForce(transform.forward * speed * -moveVertical * strafeVar);
        }

        if (useController)
        {
            if (ActiveController.Action1 && isGrounded)
            {
                rb.AddForce(transform.up * jumpStrength, ForceMode.Impulse);
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(transform.up * jumpStrength, ForceMode.Impulse);
            }
        }
        

        lookDirection = new Vector3(0, lookHorizontal, 0);
        headDirection = new Vector3(lookVertical, 0, 0);
        transform.Rotate(lookDirection * lookSpeed);
        head.transform.Rotate(headDirection * lookSpeed);
    }
}
