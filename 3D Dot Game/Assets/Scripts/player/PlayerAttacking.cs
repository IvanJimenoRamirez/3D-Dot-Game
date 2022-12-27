using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking: MonoBehaviour
{

    public Transform rightHandPlayer;
    public GameObject bigSword, littleSword;
    GameObject activeBigSword, activeLittleSword;

    Transform player;

    public float attackSpeed = 1.3f;
    float timeToAttack;

    // Start is called before the first frame update
    void Start()
    {
        // Define the time to attack speed
        timeToAttack = 1f / attackSpeed;

        // Find the player
        player = GameObject.Find("player(Clone)").transform;
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
            // Start the audio
            GetComponent<AudioSource>().Play();
            // Instantiate the big sword in front of the player
            activeBigSword = Instantiate(whichSword(), rightHandPlayer.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
            activeBigSword.transform.rotation = Quaternion.Euler(90, player.rotation.eulerAngles.y, 0);
            // invoke the "StopAttacking" function after 0.3 seconds            
            Invoke("StopAttacking", 0.3f);
        }
    }

    void StopAttacking()
    {
        // modify the animator to set the "attacking" parameter to false
        GetComponent<Animator>().SetBool("attacking", false);
        // Destroy the big sword
        Destroy(activeBigSword);
        activeBigSword = null;
    }

    public float getTimeToAttack() {
        return timeToAttack;
    }

    /*
     * Determines which sword to instantiate taking into account the player's health
     */
    private GameObject whichSword()
    {
        int health = player.GetComponent<PlayerBehaviour>().health;
        if (health == 10)
        {
            return bigSword;
        }
        return littleSword;
    }
}
