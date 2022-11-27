using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking: MonoBehaviour
{

    public Transform rightHandPlayer;

    public float attackSpeed = 1.3f;
    float timeToAttack;

    // Start is called before the first frame update
    void Start()
    {
        // Define the time to attack speed
        timeToAttack = 1f / attackSpeed;
    }

    // Function called when the attack animation starts
    void AttackStart()
    {
        if (rightHandPlayer.GetChild(0) != null)
        {
            // Set the collider to enabled
            rightHandPlayer.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    // Function called when the attack animation ends
    void AttackEnd()
    {
        if (rightHandPlayer.GetChild(0) != null)
        {
            // Set the collider to disabled
            rightHandPlayer.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeToAttack -= Time.deltaTime;
        bool isDead = GetComponent<Animator>().GetBool("dead");
        if (Input.GetMouseButtonDown(0) && timeToAttack <= 0 && !isDead)
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
