using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using System.Net;
#endif

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;

    #region Camera Movement Variables

    public Camera playerCamera;

    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    #endregion

    #region Movement Variables

    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;

    // Internal Variables
    private bool isWalking = false;

    #region Jump

    public bool enableJump = true;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 5f;

    // Internal Variables
    private bool isGrounded = false;

    #endregion

    #region Crouch

    public bool enableCrouch = true;
    public bool holdToCrouch = true;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float crouchHeight = .75f;
    public float speedReduction = .5f;

    // Internal Variables
    private bool isCrouched = false;
    private Vector3 originalScale;

    #endregion
    #endregion

    #region Head Bob

    public bool enableHeadBob = true;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Variables
    private Vector3 jointOriginalPos;
    private float timer = 0;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Set internal variables
        playerCamera.fieldOfView = fov;
        originalScale = transform.localScale;
        jointOriginalPos = joint.localPosition;
    }

    void Start()
    {
       
    }

    private void Update()
    {
        #region Camera

        // Control camera movement
        if (cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        #endregion

        #region Jump

        // Gets input and calls jump method
        if (enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        #endregion

        #region Crouch

        if (enableCrouch)
        {
            if (Input.GetKeyDown(crouchKey) && !holdToCrouch)
            {
                Crouch();
            }

            if (Input.GetKeyDown(crouchKey) && holdToCrouch)
            {
                isCrouched = false;
                Crouch();
            }
            else if (Input.GetKeyUp(crouchKey) && holdToCrouch)
            {
                isCrouched = true;
                Crouch();
            }
        }

        #endregion

        CheckGround();

        if (enableHeadBob)
        {
            HeadBob();
        }
    }

    void FixedUpdate()
    {
        #region Movement

        if (playerCanMove)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Checks if player is walking and isGrounded
            // Will allow head bob
            if (targetVelocity.x != 0 || targetVelocity.z != 0 && isGrounded)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            // All movement calculations while walking
            targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Controls player sliding down slopes
            if (Physics.Raycast(transform.position, Vector3.down, 2f) && !isGrounded)
            {
                rb.AddForce(Vector3.down * 20, ForceMode.VelocityChange);
            }

            // Applies friction to player while walking
            if (isGrounded)
            {
                rb.drag = 5f;

                if (isWalking)
                {
                    // Applies force to player if headbobbing is enabled
                    if (enableHeadBob)
                    {
                        rb.AddForce(transform.right * Mathf.Sin(Time.time * bobSpeed) * bobAmount.x);
                        rb.AddForce(transform.up * Mathf.Sin(Time.time * bobSpeed) * bobAmount.y);
                        rb.AddForce(transform.forward * Mathf.Sin(Time.time * bobSpeed) * bobAmount.z);
                    }
                }
                else
                {
                    // If player is not walking, it applies additional friction to simulate slowing down
                    rb.drag = 10f;
                }
            }
            else
            {
                // If player is airborne, apply less drag
                rb.drag = 0f;
            }
        }

        #endregion
    }

    #region Jump

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGrounded = false;
    }

    #endregion

    #region Crouch

    void Crouch()
    {
        if (enableCrouch)
        {
            if (!isCrouched)
            {
                // Crouch
                playerCamera.fieldOfView = fov - 20f;
                transform.localScale = originalScale / 1.5f;
                isCrouched = true;
            }
            else
            {
                // Uncrouch
                playerCamera.fieldOfView = fov;
                transform.localScale = originalScale;
                isCrouched = false;
            }
        }
    }

    #endregion

    #region HeadBob

    void HeadBob()
    {
        if (isWalking)
        {
            float waveslice = 0.0f;

            if (Mathf.Abs(rb.velocity.x) == 0 && Mathf.Abs(rb.velocity.z) == 0)
            {
                timer = 0.0f;
            }
            else
            {
                waveslice = Mathf.Sin(timer);
                timer = timer + bobSpeed;

                if (timer > Mathf.PI * 2)
                {
                    timer = timer - (Mathf.PI * 2);
                }
            }

            Vector3 v3T = new Vector3(jointOriginalPos.x, jointOriginalPos.y + waveslice * bobAmount.y, jointOriginalPos.z);
            joint.localPosition = v3T;
        }
        else
        {
            // If not walking, set headbob position to original
            joint.localPosition = jointOriginalPos;
        }
    }

    #endregion

    #region Ground Check

    void CheckGround()
    {
        RaycastHit hit;

        // Debug raycast
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 0.1f);

        // Check if player is grounded
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    #endregion
}
