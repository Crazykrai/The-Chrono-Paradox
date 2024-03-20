using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<Enemy> enemies;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
