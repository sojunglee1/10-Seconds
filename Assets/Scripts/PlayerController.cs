using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject camera;
    float movementSpeed = 5;
    [SerializeField] float walkSpeed = 10;
    [SerializeField] float runSpeed = 20;
    [SerializeField] float jumpForce = 500;
    [SerializeField] float turnSpeed = 1f;

    [Header("Movement Checks")]
    [SerializeField] bool isRunning = false;
    [SerializeField] bool isJumping = false;

    Vector2 movementInput = Vector2.zero;
    Vector2 rotationInput = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    Animator animator;
    Rigidbody rigidBody;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!(movementInput.magnitude > 0 || rotationInput.magnitude > 0))
        {
            moveDirection = Vector3.zero;
            rotationInput = Vector3.zero;
            animator.SetInteger("AnimationState", 0);
            return;
        }
        if (!isRunning)
        {
            movementSpeed = walkSpeed;
            animator.SetInteger("AnimationState", 1);
        }
        else
        {
            movementSpeed = runSpeed;
            animator.SetInteger("AnimationState", 2);
        }

        moveDirection = transform.forward * movementInput.y + transform.right * movementInput.x;
        Vector3 movementDirection = moveDirection * (movementSpeed * Time.deltaTime);
        transform.position += movementDirection;

        if (rotationInput.magnitude > 0)
        {
            if (rotationInput.x > 0)
            {
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

            }
            else if (rotationInput.x < 0)
            {
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            }
        }
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    public void OnRotate(InputValue value)
    {
        rotationInput = value.Get<Vector2>();
    }

    public void OnRun(InputValue value)
    {
        isRunning = !isRunning;
    }

    public void OnJump()
    {
        rigidBody.AddForce(0, jumpForce, 0);
        isJumping = true;
    }

    public void OnPause()
    {
        GameManager.instance.PauseGame();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
        }

        else if (collision.gameObject.tag == "Key")
        {
            GetComponent<AudioSource>().Play();
            GameManager.instance.FoundKey = true;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Door")
        {
            GetComponent<AudioSource>().Play();
            GameManager.instance.FoundDoor = true;
            if (GameManager.instance.FoundKey)
            {
                GameManager.instance.GameWon = true;
            }
        }
    }
}
