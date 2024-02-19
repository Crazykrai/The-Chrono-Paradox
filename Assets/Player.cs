using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public Animator animator;
    private Vector3 mousePosition;

    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        animator.SetFloat("speed", (Mathf.Abs(moveInput.y)+Mathf.Abs(moveInput.x)/2));
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        mousePosition = Input.mousePosition;
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        print("click!");
            animator.SetTrigger("click");
    }
}
