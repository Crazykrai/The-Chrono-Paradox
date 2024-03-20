using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 movementVector;
    private float movementX;
    private float movementY;
    public float speed;
    public GameObject playerCharacter;

    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.instance.enemies.Add(this);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementValue.Get());
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector2 playerDirection;
        
        if(playerCharacter.transform.position.y < gameObject.transform.position.y)
        {
            playerDirection.y = -1;
        } else
        {
            playerDirection.y = 1;
        }

        if (playerCharacter.transform.position.x < gameObject.transform.position.x)
        {
            playerDirection.x = -1;
        }
        else
        {
            playerDirection.x = 1;
        }

        Vector2 playerDistance;
        playerDistance.x = (playerCharacter.transform.position.x - gameObject.transform.position.x);
        playerDistance.y = (playerCharacter.transform.position.y - gameObject.transform.position.y);

        if(playerDistance.magnitude > 1)
        {
            rb.MovePosition(rb.position + playerDirection * speed * Time.fixedDeltaTime);
        }
    }

    private void OnDestroy()
    {
        EnemyManager.instance.enemies.Remove(this);
    }
}
