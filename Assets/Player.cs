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
    private Vector3 inputVector;
    public SpriteRenderer sr;
    private HealthBarController hbc;
    private void Start()
    {
       sr = GetComponent<SpriteRenderer>();
       rb = GetComponent<Rigidbody2D>();
       hbc = GetComponent<HealthBarController>();
    }
    void FixedUpdate()
    {
        animator.SetFloat("speed", (Mathf.Abs(moveInput.y)+Mathf.Abs(moveInput.x)/2));
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        inputVector = mousePosition - rb.transform.position;
        inputVector = inputVector.normalized;
        if (mousePosition.x > rb.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("click");
            
    }
}