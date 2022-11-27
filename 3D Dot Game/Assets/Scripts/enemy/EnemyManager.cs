using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // On collide
    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy collides with the sword, then destroy the enemy
        if (health > 0)
        {
            if (collision.gameObject.tag == "BigSwordP")
            {
                health -= 2;
            }
            if (collision.gameObject.tag == "LittleSwordP")
            {
                health--;
            }
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
