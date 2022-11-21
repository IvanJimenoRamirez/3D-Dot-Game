using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    public float attackSpeed = 1.5f;
    float timeToAttack;


    
    // Start is called before the first frame update
    void Start()
    {
        timeToAttack = 1f / attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timeToAttack -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timeToAttack <= 0)
        {
            timeToAttack = 1f / attackSpeed;
            // modify the animator to set the "attacking" parameter to true
            GetComponent<Animator>().SetBool("attacking", true);
            // invoke the "StopAttacking" function after 0.5 seconds
            
            GetComponent<AudioSource>().Play();
            Invoke("StopAttacking", 0.3f);

        }
    }

    void StopAttacking()
    {
        // modify the animator to set the "attacking" parameter to false
        GetComponent<Animator>().SetBool("attacking", false);
    }

    public float getTimeToAttack() {
        return timeToAttack;
    }
}
