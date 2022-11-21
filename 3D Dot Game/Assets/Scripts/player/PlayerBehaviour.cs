using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int health = 9;
    public int maxHealth = 10;

    //public GameObject[] healthIcons, healthIconsEmpty;
    public GameObject rightHand, littleSword, bigSword, healthText;

    // TODO: ticks per second ==> Determines how often the player takes damage
    public float ticksPerSecond;

    void destroyPlayer()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Add the bigSword to the rightHand as the first child
        Instantiate(bigSword, rightHand.transform);
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is dead then destroy the player
        if (health <= 0)
        {
            Invoke("destroyPlayer", 1.5f);
        }
        // Change the sword to the little one
        if (health < maxHealth && rightHand.transform.GetChild(0).gameObject.name != "littleSword")
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            Instantiate(littleSword, rightHand.transform);
        }
        // If the player has the maxHealth && the sword in the right hand is the little one, then replace it with the big one
        if (health == maxHealth && rightHand.transform.GetChild(0).gameObject.name != "bigSword")
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            Instantiate(bigSword, rightHand.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides with an enemy, then take damage
        if (collision.gameObject.tag == "Enemy" && health > 0)
        {
            health--;
            // Update the health TextMeshPro - Text
            healthText.GetComponent<TMPro.TextMeshPro>().text = "HEALTH: " + health.ToString();

            if (health == 0)
            {
                GetComponent<Animator>().SetBool("dead", true);
            }
        }
        // If the player collides with a health pickup, then heal
        if (collision.gameObject.tag == "HealthPickup")
        {
            health++;
            healthText.GetComponent<TMPro.TextMeshPro>().text = "HEALTH: " + health.ToString();
            Destroy(collision.gameObject);
        }
        // If the player collides with a wall, then stop moving
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Collided with a wall");
            //Refactor
            GetComponent<PlayerMovement>().stopMoving();
        }
    }
}
