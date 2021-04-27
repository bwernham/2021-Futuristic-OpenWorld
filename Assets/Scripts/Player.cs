using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public bool groundedPlayer;

    public float walkSpeed = 6f;
    public float runSpeed = 10f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 velocity;

    public float jumpHeight;
    public float gravity;

    private float tapSpeed = 0.5f;
    private float lastTapTime;
    private bool doubleTap;

    void Start()
    {
        Screen.lockCursor = true;
        Cursor.visible = false;
        lastTapTime = 0f;
    }

    void Update()
    {
        groundedPlayer = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (groundedPlayer && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetKeyDown(KeyCode.W))
            {
                if ((Time.time - lastTapTime) < tapSpeed)
                {
                    doubleTap = true;

                    if (Input.GetKeyUp(KeyCode.W)) // Character still runs when moving backward or side to side
                    {
                        doubleTap = false;
                    }
                }
                else
                {
                    doubleTap = false;
                }

                lastTapTime = Time.time;
            }

            if (doubleTap)
            {
                controller.Move(moveDir.normalized * runSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
            }
        }
    }
}
