using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float walkForce;
    [SerializeField] float runForce;
    [SerializeField] float jumpForce;
    [SerializeField] float walkStepRange;
    [SerializeField] float runStepRange;
    [SerializeField] bool debug;

    CharacterController controller;
    Vector3 moveDir;
    private float ySpeed;
    private bool isWalk;
    Animator animator;
    private float curSpeed;
    float curStepRange;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        Fall();
    }

    float lastStepTime = 0.5f;
    private void Move()
    {
        if (moveDir.magnitude == 0)
        {
            curSpeed = Mathf.Lerp(curSpeed, 0, 0.1f);
            animator.SetFloat("MoveSpeed", curSpeed);
            curStepRange = 0;
            return;
        }
        
        Vector3 forwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;

        if (isWalk)
        {
            curSpeed = Mathf.Lerp(curSpeed, walkForce, 0.1f);
            curStepRange = walkStepRange;
        }
        else
        {
            curSpeed = Mathf.Lerp(curSpeed, runForce, 0.1f);
            curStepRange = runStepRange;
        }

        controller.Move(forwardVec * moveDir.z * curSpeed * Time.deltaTime);
        controller.Move(rightVec * moveDir.x * curSpeed * Time.deltaTime);
        animator.SetFloat("MoveSpeed", curSpeed);
        Quaternion lookRotation = Quaternion.LookRotation(forwardVec * moveDir.z + rightVec * moveDir.x);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

        lastStepTime -= Time.deltaTime;
        if (lastStepTime < 0)
        {
            lastStepTime = 0.5f;
            GenerateFootStepSound();
        }
    }

    private void GenerateFootStepSound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, curStepRange);
        foreach (Collider collider in colliders) 
        {
            IListenable listenable = collider.GetComponent<IListenable>();
            listenable?.Listen(transform);
        }
    }
    private void OnMove(InputValue input)
    {
        moveDir.x = input.Get<Vector2>().x;
        moveDir.z = input.Get<Vector2>().y;
    }
    private void Jump()
    {
        ySpeed = jumpForce;
    }
    private void OnJump()
    {
        Jump();
    }
    private void Fall()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if(controller.isGrounded && ySpeed < 0)
            ySpeed = 0;

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }
    private void OnWalk(InputValue input)
    {
        isWalk = input.isPressed;
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkStepRange);
        Gizmos.DrawWireSphere(transform.position, runStepRange);
    }
}
