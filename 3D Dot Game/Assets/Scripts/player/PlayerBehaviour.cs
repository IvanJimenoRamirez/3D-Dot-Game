using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int health = 9;
    public int maxHealth = 10;
    public bool hasKey = false;

    public GameObject rightHand, littleSword, bigSword;

    // Public attributes for the player's UI
    public Canvas heartIcons;
    public GameObject emptyHeart, filledHeart;
    
    // TODO: ticks per second ==> Determines how often the player takes damage
    public float ticksPerSecond;
    float timeToTick;
    
    void destroyPlayer()
    {
        Destroy(gameObject);
    }
    
    void playerUI()
    {
        // Update the canva to show the player's health
        heartIcons.GetComponent<Canvas>().enabled = true;
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < health)
            {
                filledHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(28 * (i + 1), -28);
                Instantiate(filledHeart, heartIcons.transform);
            }
            else
            {
                emptyHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(28 * (i + 1), -28);
                Instantiate(emptyHeart, heartIcons.transform);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find the canvas from the scene hierarchy
        heartIcons = GameObject.Find("HeartUI").GetComponent<Canvas>();
        // Add the bigSword to the rightHand as the first child
        Instantiate(bigSword, rightHand.transform);
        // Instantiate the heart icons
        playerUI();
        // Set the time to tick
        timeToTick = 1f / ticksPerSecond;
    }
    
    private void setUpSwordColliders()
    {
        rightHand.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the time to tick
        if (timeToTick > 0f) timeToTick -= Time.deltaTime;

        // If the player is dead then destroy the player
        if (health <= 0)
        {
            Invoke("destroyPlayer", 1.5f);
        }
        // Change the sword to the little one
        if (health < maxHealth && rightHand.transform.GetChild(0).gameObject.name != "littleSword(Clone)")
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            Instantiate(littleSword, rightHand.transform);
            setUpSwordColliders();
        }
        // If the player has the maxHealth && the sword in the right hand is the little one, then replace it with the big one
        if (health == maxHealth && rightHand.transform.GetChild(0).gameObject.name != "bigSword(Clone)")
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            Instantiate(bigSword, rightHand.transform);
            setUpSwordColliders();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides with an enemy, then take damage
        if (collision.gameObject.tag == "Enemy" && health > 0)
        {
            if (timeToTick <= 0f)
            {
                health--;
                timeToTick = 1f / ticksPerSecond;

                // TODO: start the "TakeDamage" animation

                // Change the heart icon in the "health" position to the empty heart
                heartIcons.transform.GetChild(health).GetComponent<UnityEngine.UI.Image>().sprite = emptyHeart.GetComponent<UnityEngine.UI.Image>().sprite;
            }

            if (health == 0)
            {
                GetComponent<Animator>().SetBool("dead", true);
            }
        }
        // If the player collides with a health pickup, then heal
        if (collision.gameObject.tag == "HealthPickup")
        {
            health++;
            // Change the heart icon in the "health" position to the filled heart
            heartIcons.transform.GetChild(health - 1).GetComponent<UnityEngine.UI.Image>().sprite = filledHeart.GetComponent<UnityEngine.UI.Image>().sprite;
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidBody = hit.collider.attachedRigidbody;

        if (rigidBody != null)
        {
            Vector3 forceDir = hit.gameObject.transform.position - transform.position;
            forceDir.y = 0;
            forceDir.Normalize();
            rigidBody.AddForceAtPosition(forceDir * 1, transform.position, ForceMode.Impulse);
        }
    }
}
