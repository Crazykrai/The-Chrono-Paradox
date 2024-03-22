using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float health = 20;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if(anim)
            {
                anim.SetTrigger("killed");
            } else
            {
                Destroy(gameObject);
            }

        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
