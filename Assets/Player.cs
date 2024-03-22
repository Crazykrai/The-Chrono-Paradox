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
    private bool hitboxActive = false;
    public GameObject hitboxPoint;
    public GameObject swordHitbox;
    private GameObject currentSwordHit;
    private BoxCollider2D collider2D;

    bool isInvulnerable = false;
    float invulnerabilityDuration = 1.0f; // Adjust this as needed
    float invulnerabilityTimer = 0.0f;

    private void Start()
    {
       sr = GetComponent<SpriteRenderer>();
       rb = GetComponent<Rigidbody2D>();
       hbc = GetComponent<HealthBarController>();
    }
    void FixedUpdate()
    {
        animator.SetFloat("speed", (Mathf.Abs(moveInput.y) + Mathf.Abs(moveInput.x) / 2));
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        inputVector = mousePosition - rb.transform.position;
        inputVector = inputVector.normalized;
        if(!hitboxActive)
        {
            if (mousePosition.x > rb.position.x)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
        
        
        if (animator.GetBool("click"))
        {
            if (!hitboxActive)
            {
                hitboxActive = true;
                currentSwordHit = Instantiate(swordHitbox, hitboxPoint.transform);
                if(sr.flipX.Equals(true)) currentSwordHit.transform.Rotate(new Vector3(0, 180, 0));
                //swordHitbox.transform.position.Set(swordHitbox.transform.position.x,hitboxPoint.transform.position.y,swordHitbox.transform.position.z);
            }
        }

        if (animator.GetFloat("HP") == 0)
        {
            moveSpeed = 0f;
        }

        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;

            // If invulnerability period is over, make the player vulnerable again
            if (invulnerabilityTimer <= 0.0f)
            {
                isInvulnerable = false;
            }
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        print("click!");
        if(!animator.GetBool("melee"))
        {
            animator.SetTrigger("click");
        }
        
        animator.SetBool("melee", true);
    }

    public void meleeComplete()
    {
        animator.SetBool("melee", false);
        Destroy(currentSwordHit);
        hitboxActive = false;
    }
}