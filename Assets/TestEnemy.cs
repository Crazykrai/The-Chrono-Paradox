using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestEnemy : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movementVector;
    private float movementX;
    private float movementY;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementValue.Get());
        movementX = movementVector.x;
        movementY = movementVector.y;
        if(movementY > 0)
        {
            anim.SetBool("MovingDown", false);
            anim.SetBool("MoveUp", true);
        } else if(movementY < 0)
        {
            anim.SetBool("MovingDown", true);
            anim.SetBool("MoveUp", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementVector * speed * Time.fixedDeltaTime);
    }
}
