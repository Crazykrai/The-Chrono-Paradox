using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    //public static SwordAttack instance;
    public Sprite player;
    private float damage = 5;

    private void Awake()
    {
        /*if(instance == null)
        {
            instance = this;
        } else
        {
            print("SwordAttack already active!");
        }*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger collision detected!!");

        print(other);

        Life life = other.GetComponent<Life>();
        if(life)
        {
            life.health -= damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
